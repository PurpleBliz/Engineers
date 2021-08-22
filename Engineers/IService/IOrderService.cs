using System.Collections.Generic;
using Engineers.Models;
using Microsoft.AspNetCore.Http;

namespace Engineers.IService
{
    public interface IOrderService
    {
        public Response GetAll();

        public Response GetOpen();

        public Response GetInWork();

        public Response GetById(int OrderId);

        public Response GetByUser(string userId);

        public Response GetReviews(int OrderId);

        public Response SelectExecutor(Order oOrder, User user);

        public Response Create(Order oOrder);
               
        public Response Update(Order oOrder);

        public Response UploadImage(IFormFileCollection files);
               
        public Response Delete(int OrderId);

        public Response GetBlueprints();
    }
}
