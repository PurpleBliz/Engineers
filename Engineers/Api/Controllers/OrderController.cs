using Microsoft.AspNetCore.Mvc;
using Engineers.Models;
using Engineers.IService;
using Microsoft.AspNetCore.Http;

namespace Engineers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAll")]
        public Response GetAll() => _orderService.GetAll();

        [HttpGet("GetOpen")]
        public Response GetOpen() => _orderService.GetOpen();

        [HttpGet("GetInWork")]
        public Response GetInWork() => _orderService.GetInWork();

        [HttpGet("Get/{id}")]
        public Response GetOrder(int id) => _orderService.GetById(id);

        [HttpGet("GetByUser")]
        public Response GetByUser(string userId) => _orderService.GetByUser(userId);

        [HttpPost("Create")]
        public Response Add(Order oOrder) => _orderService.Create(oOrder);

        [HttpPost("UpLoadImage/{id}")]
        public Response UpLoadImage(int id, IFormFileCollection files)
        {
            Order order;

            Response response = _orderService.GetById(id);

            if (response.Success)
                order = (Order)response.Data;
            else return response;

            response = _orderService.UploadImage(files);

            if (response.Success)
                order.Images = (string)response.Data;
            else return response;

            return _orderService.Update(order);
        }

        [HttpPut("Update/{id}")]
        public Response Update(Order oOrder) => _orderService.Update(oOrder);

        [HttpDelete("Delete/{id}")]
        public Response Remove(int id) => _orderService.Delete(id);
    }
}
