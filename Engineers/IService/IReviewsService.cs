using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineers.Models;
using Microsoft.AspNetCore.Http;

namespace Engineers.IService
{
    public interface IReviewsService
    {
        public Response Get();

        public Response GetById(int id);

        public Response Create(Review review);

        public Response Update(int id);

        public Response Delete(int id);
    }
}
