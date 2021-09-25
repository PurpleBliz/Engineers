using System;
using Engineers.IService;
using Engineers.Models;
using Engineers.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Engineers.Api.Service
{
    public class ReviewsService : IReviewsService
    {
        public readonly ApplicationContext _context;

        private readonly Response response;

        public ReviewsService(ApplicationContext context)
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

        public Response GetByOrder(int orderId)
        {
            response.Data = _context.Reviews.Include(r => r.User)
                .Include(r => r.Order).Where(r => r.OrderId == orderId).ToList();

            return response;
        }

        public Response GetByUser(string userId)
        {
            response.Data = _context.Reviews.Include(r => r.User)
                .Include(r => r.Order).Where(r => r.UserId == userId).ToList();

            return response;
        }

        public Response GetById(int id)
        {
            response.Data = _context.Reviews.Include(r => r.User)
     .Include(r => r.Order).FirstOrDefault(r => r.Id == id);

            if (response.Data is null)
                return response.ReturnBADResponse("Отзывов пока нет");

            return response;
        }

        public Response Create(Review review)
        {
            var reviews = _context.Reviews.Include(r => r.User)
    .Include(r => r.Order).Where(r => r.OrderId == review.OrderId).ToList();

            foreach (var item in reviews)
                if (item.UserId == review.UserId)
                    return response.ReturnBADResponse("Отзыв уже оставлен", item);

            review.Updated_at = DateTime.Now;
            review.Created_at = DateTime.Now;

            _context.Reviews.Add(review);

            _context.SaveChanges();

            response.Data = review;

            return response;
        }

        public Response Update(int id)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response Get()
        {
            throw new NotImplementedException();
        }
    }
}