using System;
using Engineers.IService;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Engineers.Models;
using Engineers.Context;
using System.Linq;
using Newtonsoft.Json;

namespace Engineers.Service
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

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response Get()
        {
            throw new NotImplementedException();
        }

        public Response GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Response Create(Review review)
        {
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
    }
}