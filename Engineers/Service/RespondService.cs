using System;
using Engineers.IService;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Engineers.Models;
using Engineers.Context;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Engineers.Service
{
    public class RespondService : IRespondService
    {
        public readonly ApplicationContext _context;

        private readonly Response response;

        public RespondService(ApplicationContext context)
        {
            _context = context;

            response = new()
            {
                Code = 200,
                Text = "OK",
                Data = null,
                Success = true
            };
        }

        public Response Delete(int id)
        {
            response.Data = _context.Responds.FirstOrDefault(r => r.Id == id);

            if (response.Data is null)
                return response.ReturnBADResponse("Отклик не найден");

            _context.Responds.Remove(response.Data as Respond);

            _context.SaveChanges();

            response.Data = null;

            return response;
        }

        public Response Get()
        {
            response.Data = _context.Responds.Include(r => r.User).Include(r => r.Order).ToList();

            if (response.Data is null)
                return response.ReturnBADResponse("Откликов пока нет");

            return response;
        }

        public Response GetByUserId(string userId)
        {
            response.Data = _context.Responds.Include(r => r.User).Include(r => r.Order).FirstOrDefault(r => r.UserId == userId);

            if (response.Data is null)
                return response.ReturnBADResponse("Откликов пока нет");

            return response;
        }

        public Response GetByOrderId(int orderId)
        {
            response.Data = _context.Responds.Include(r => r.Order).Include(r => r.User).FirstOrDefault(r => r.OrderId == orderId);

            if (response.Data is null)
                return response.ReturnBADResponse("Откликов пока нет");

            return response;
        }

        public Response GetById(int id)
        {
            response.Data = _context.Responds.Include(r => r.User).Include(r => r.Order).FirstOrDefault(r => r.Id == id);

            if (response.Data is null)
                return response.ReturnBADResponse("Откликов пока нет");

            if ((response.Data as Respond).User is null)
                response.Text = "У данного отклика отсутствует пользователь";

            if ((response.Data as Respond).Order is null)
                response.Text = "У данного заказа отсутствует Заказ";

            return response;
        }

        public Response Create(Respond respond)
        {
            _context.Responds.Add(respond);

            _context.SaveChanges();

            response.Data = respond;

            return response;
        }

        public Response Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}