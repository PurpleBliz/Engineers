using Engineers.Models;

namespace Engineers.Api.IService
{
    public interface IReviewsService
    {
        public Response Get();

        public Response GetByOrder(int orderId);

        public Response GetByUser(string userId);

        public Response GetById(int id);

        public Response Create(Review review);

        public Response Update(int id);

        public Response Delete(int id);
    }
}
