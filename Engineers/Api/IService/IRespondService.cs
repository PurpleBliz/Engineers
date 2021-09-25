using Engineers.Models;

namespace Engineers.Api.IService
{
    public interface IRespondService
    {
        public Response Get();

        public Response GetById(int id);

        public Response GetByUserId(string userId);

        public Response GetByOrderId(int orderId);

        public Response Create(Respond respond);

        public Response Update(int id);

        public Response Delete(int id);
    }
}
