using Engineers.Api.Models;
using Engineers.Models;
using Microsoft.AspNetCore.Http;

namespace Engineers.Api.IService
{
    public interface IOrderService
    {
        public Response GetAll();

        public Response GetOpen();

        public Response GetInWork();

        public Response GetComplited();

        public Response GetById(int OrderId);

        public Response Complited(int orderId);

        public Response GetByUser(string userId);

        public Response GetResponds(int orderId);

        public Response SendRespond(Respond respond);

        public Response SelectExecutor(ApiOrder apiOrder, string userId);

        public Response Create(ApiOrder apiOrder);
               
        public Response Update(ApiOrder apiOrder, int id);

        public Response UploadImage(IFormFileCollection files);
               
        public Response Delete(int OrderId);

        public Response GetBlueprints();
    }
}
