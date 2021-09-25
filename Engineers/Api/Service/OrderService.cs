using System;
using System.Collections.Generic;
using System.Linq;
using Engineers.Context;
using Engineers.Models;
using Engineers.Api.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Engineers.Api.Models;

namespace Engineers.Api.Service
{
    public class OrderService : IOrderService
    {
        private readonly int DefaultState = 1;

        private readonly ApplicationContext _context;
        private readonly Engineers.IService.IFileService _fileService;

        private readonly Response response = new();

        public OrderService(ApplicationContext context, Engineers.IService.IFileService fileService)
        {
            _fileService = fileService;
            _context = context;

            response = new()
            {
                Code = 200,
                Text = "OK",
                Data = null,
                Success = true
            };
        }

        public Response GetAll()
        {
            response.Data = _context.Orders.Include(o => o.Owner).ToList().ConverToListApiOrder();

            if ((response.Data as List<ApiOrder>).Count <= 0)
                response.Text = "Заказов пока нет";

            return response;
        }

        public Response Complited(int orderId)
        {
            response.Data = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            var order = (Order)response.Data;

            if (order is null)
                return response.ReturnBADResponse("Заказ не найден");

            order.State = 0;

            var remove = _context.OrdersInWorks.FirstOrDefault(o => o.OrderId == orderId);

            if (remove is null)
                return response.ReturnBADResponse("Невозможно пометить заказ выполненным выберите исполнителя");

            _context.Orders.Update(order);

            _context.SaveChanges();

            return response;
        }

        public Response GetComplited()
        {
            response.Data = _context.Orders
                .Include(o => o.InWork)
                .Where(o => o.State == 0 && o.InWork != null)
                .ToList().ConverToListApiOrder();

            if ((response.Data as List<ApiOrder>).Count <= 0)
                response.Text = "Завершенных заказов пока нет";

            return response;
        }

        public Response GetById(int orderId)
        {
            response.Data = _context.Orders.Include(u => u.Owner)
                .Include(u => u.InWork)
                .FirstOrDefault(x => x.Id == orderId)
                .ConverToApiOrder();

            if (response.Data is null)
                return response.ReturnBADResponse("Заказ не найден! или бы загружен с ошибкой");

            return response;
        }

        public Response GetByUser(string userId)
        {
            response.Data = _context.Orders.Include(u => u.Owner)
                .Where((o => o.Owner.Id == userId))
                .ToList()
                .ConverToListApiOrder();

            if (response.Data is null)
                return response.ReturnBADResponse("Заказ не найден!");

            return response;
        }

        public Response GetOpen()
        {
            response.Data = _context.Orders.Include(o => o.Owner)
                .Where(o => o.State == 1)
                .ToList().ConverToListApiOrder();

            if ((response.Data as List<ApiOrder>).Count <= 0)
                response.Text = "Открытых заказов пока нет";

            return response;
        }

        public Response GetInWork()
        {
            response.Data = _context.Orders.Include(o => o.Owner)
                .Include(o => o.InWork)
                .Where(o => o.State == 1 && o.InWork != null)
                .ToList().ConverToListApiOrder();

            if ((response.Data as List<ApiOrder>).Count <= 0)
                response.Text = "Заказов в работе пока нет";

            return response;
        }

        public Response Update(ApiOrder apiOrder, int id)
        {
            Order order = apiOrder.ConverToOrder();

            var oldOrder = _context.Orders.Find(id);

            order.Updated_at = DateTime.Now;

            _context.Entry(oldOrder).CurrentValues.SetValues(order);

            _context.SaveChanges();

            response.Data = apiOrder;

            return response;
        }

        public Response GetResponds(int orderId)
        {
            response.Data = _context.Responds.Where(r => r.OrderId == orderId).ToList();

            if ((response.Data as List<Respond>).Count <= 0)
                response.Text = "Откликов пока нет";

            return response;
        }

        public Response UploadImage(IFormFileCollection files)
        {
            response.Data = _fileService.UploadArray(files).ConvertListToString();

            return response;
        }

        public Response GetBlueprints()
        {
            response.Data = _fileService.GetBlueprints();

            return response;
        }

        public Response GetReviews(int orderId)
        {
            response.Data = _context.Orders.Include(o => o.Reviews);

            if (response.Data is null)
                return response.ReturnBADResponse("Заказ не найден");

            if ((response.Data as Order).Reviews is null)
                response.Text = "У данного заказа отсутствуют отзывы";

            return response;
        }

        public Response SelectExecutor(ApiOrder apiOrder, string userId)
        {
            var order = apiOrder.ConverToOrder();

            if (order is null)
                response.ReturnBADResponse("Заказ не выбран !");

            var user = _context.Users.Find(userId);

            if (user is null)
                response.ReturnBADResponse("Исполнитель не найден !");

            order.State = 0;

            OrdersInWork ordersInWork = new()
            {
                OrderId = order.Id,
                Executor = user,
                Started_at = DateTime.Now
            };

            response.Data = ordersInWork;

            _context.Orders.Update(order);

            _context.OrdersInWorks.Add(ordersInWork);

            _context.SaveChanges();

            return response;
        }

        public Response SendRespond(Respond respond)
        {
            respond.Created_at = DateTime.Now;

            _context.Responds.Add(respond);

            _context.SaveChanges();

            return GetResponds(respond.OrderId); 
        }

        public Response Create(ApiOrder apiOrder)
        {
            if (!apiOrder.IsValid())
                return response.ReturnBADResponse("Model is not validate");

            var user = _context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == apiOrder.OwnerId);

            if (user is null)
                return response.ReturnBADResponse("Пользователь не найден");

            if (user.Orders.Count > 0 || user.Orders != null)
                foreach (var order in user.Orders)
                    if (order.Name == apiOrder.Name)
                        return response.ReturnBADResponse("Заказ с таким именем уже существует", order);

            apiOrder.State = DefaultState;
            apiOrder.Created_at = DateTime.Now;
            apiOrder.Updated_at = DateTime.Now;

            _context.Orders.Add(apiOrder.ConverToOrder());

            _context.SaveChanges();

            response.Data = _context.Orders.FirstOrDefault(o => o.Name == apiOrder.Name && o.OwnerId == apiOrder.OwnerId);

            return response;
        }

        public Response Delete(int OrderId)
        {
            var Order = _context.Orders.FirstOrDefault(x => x.Id == OrderId);

            if (Order is null)
                response.ReturnBADResponse("Заказ не найден");

            _context.Orders.Remove(Order);

            _context.SaveChanges();

            response.Text = "Заказ успешно удален";

            return response;
        }
    }
}
