using Microsoft.AspNetCore.Mvc;
using Engineers.Models;
using Engineers.Api.IService;
using Microsoft.AspNetCore.Http;
using Engineers.Api.Models;

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

        [HttpGet("GetResponds")]
        public Response Get(int id) => _orderService.GetResponds(id);

        [HttpGet("GetComplited")]
        public Response GetComplited() => _orderService.GetComplited();

        [HttpPost("Create")]
        public Response Add(ApiOrder apiOrder) => _orderService.Create(apiOrder);

        [HttpPost("Complited/{id}")]
        public Response Complited(int id) => _orderService.Complited(id);

        [HttpPost("SendRespond")]
        public Response Send(Respond respond) => _orderService.SendRespond(respond);

        [HttpPost("SelectExecutor")]
        public Response SelectExecutor(ApiOrder apiOrder, string userId) => _orderService.SelectExecutor(apiOrder, userId);

        [HttpPost("UpLoadImage/{id}")]
        public Response UpLoadImage(int id, IFormFileCollection files)
        {
            ApiOrder order;

            var response = _orderService.GetById(id);

            if (response.Success)
                order = (ApiOrder)response.Data;
            else return response;

            response = _orderService.UploadImage(files);

            if (response.Success)
                order.Images = (string)response.Data;
            else return response;

            return _orderService.Update(order, id);
        }

        [HttpPut("Update/{id}")]
        public Response Update(ApiOrder apiOrder, int id) => _orderService.Update(apiOrder, id);

        [HttpDelete("Delete/{id}")]
        public Response Remove(int id) => _orderService.Delete(id);
    }
}
