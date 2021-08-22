using System;
using System.Collections.Generic;
using System.Linq;
using Engineers.Context;
using Engineers.Models;
using Engineers.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Engineers.Service
{
    public class OrderService : IOrderService
    {
        private readonly int DefaultState = 1;

        private readonly ApplicationContext _context;
        private readonly IFileService _fileService;

        private readonly Response response = new();

        public OrderService(ApplicationContext context, IFileService fileService)
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

        public Response Create(Order oOrder)
        {
            var user = _context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == oOrder.Owner.Id);

            var Orders = user.Orders;

            if (Orders != null)
            {
                foreach (var Order in Orders)
                {
                    if (oOrder.Name == Order.Name)
                    {
                        response.Success = false;
                        response.Text = "У данного пользователя уже есть заказ с таким именем";
                        response.Code = 400;

                        return response;
                    }
                }
            }

            oOrder.State = DefaultState;
            oOrder.Created_at = DateTime.Now;
            oOrder.Updated_at = DateTime.Now;

            _context.Orders.Add(oOrder);

            _context.SaveChanges();

            response.Data = oOrder;

            return response;
        }

        public Response Delete(int OrderId)
        {
            var Order = _context.Orders.FirstOrDefault(x => x.Id == OrderId);

            try
            {
                Order.State = 2;

                _context.Orders.Update(Order);

                _context.SaveChanges();

                return response;
            }
            catch(Exception ex)
            {
                response.Code = 400;
                response.Text = ex.Message;
                response.Data = OrderId;
                response.Success = false;

                return response;
            }
        }

        public Response GetAll()
        {
            response.Data = _context.Orders.Include(o => o.Owner).Include(o => o.Responds).Include(o => o.InWork).ToList();

            if ((response.Data as List<Order>).Count <= 0)
                response.Text = "Заказов пока нет";

            return response;
        }

        public Response GetById(int orderId)
        {
            var Order = _context.Orders.Include(o => o.Owner).Include(o => o.Responds).Include(o => o.InWork).FirstOrDefault(x => x.Id == orderId);

            response.Data = Order;

            if (Order is null)
                return response.ReturnBADResponse("Заказ не найден!");
            if (Order.Owner is null)
                response.Text += "; У этого заказа нет заказчика";
            if (Order.InWork is null)
                response.Text += "; Этот заказ не в работе";
            if (Order.Responds is null)
                response.Text += "; У этого заказа нет откликов";

            return response;
        }

        public Response GetByUser(string userId)
        {
            var Order = _context.Orders.Include(u => u.Owner).Include(o => o.InWork).Include(o => o.Responds).FirstOrDefault(o => o.Owner.Id == userId);

            response.Data = Order;

            if (Order is null)
                return response.ReturnBADResponse("Заказ не найден!");
            if (Order.InWork is null)
                response.Text += "; Этот заказ не в работе";
            if (Order.Responds is null)
                response.Text += "; У этого заказа нет откликов";

            return response;
        }

        public Response GetOpen()
        {
            List<Order> openOrder = new();

            _context.Orders.Include(o => o.Owner).Include(o => o.Responds).Include(o => o.InWork).ToList().ForEach(Order =>
            {
                if (Order.State == 1) openOrder.Add(Order);
            });

            response.Data = openOrder;

            if (openOrder.Count <= 0)
                response.Text = "Открытых заказов пока нет";

            return response;
        }

        public Response GetInWork()
        {
            List<Order> Orders = new();

            _context.Orders.Include(o => o.Owner).Include(o => o.Responds).Include(o => o.InWork).ToList().ForEach(Order =>
            {
                if (Order.State == 1) 
                    if (Order.InWork != null)
                        Orders.Add(Order);
            });

            if (Orders.Count <= 0)
                response.Text = "Заказов в работе пока нет";

            response.Data = Orders;

            return response;
        }

        public Response Update(Order oOrder)
        {
            var Order = _context.Orders.SingleOrDefault(x => x.Id == oOrder.Id);

            try
            {
                oOrder.Updated_at = DateTime.Now;

                _context.Orders.Update(oOrder);

                _context.SaveChanges();

                response.Data = oOrder;

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 400;
                response.Text = ex.Message;
                response.Data = oOrder;
                response.Success = false;

                return response;
            }
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

        public Response GetReviews(int OrderId)
        {
            response.Data = _context.Orders.Include(o => o.Reviews).FirstOrDefault(o => o.Id == OrderId);

            if (response.Data is null)
                return response.ReturnBADResponse("Заказ не найден");

            if ((response.Data as Order).Reviews is null)
                response.Text = "У данного заказа отсутствуют отзывы";

            return response;
        }

        public Response SelectExecutor(Order oOrder, User user)
        {
            if (oOrder is null)
                response.ReturnBADResponse("Заказ не выбран !");

            if (user is null)
                response.ReturnBADResponse("Исполнитель не выбран !");

            oOrder.State = 0;

            OrdersInWork ordersInWork = new()
            {
                OrderId = oOrder.Id,
                Executor = user,
                Started_at = DateTime.Now
            };

            response.Data = ordersInWork;

            _context.Orders.Update(oOrder);

            _context.OrdersInWorks.Add(ordersInWork);

            _context.SaveChanges();

            return response;
        }
    }
}
