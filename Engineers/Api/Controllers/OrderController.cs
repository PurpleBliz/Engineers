using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Engineers.Models;
using Engineers.IService;
using System.Collections.Generic;

namespace Engineers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService = null;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Order> GetAll()
        {
            return _orderService.GetAll();
        }

        [HttpGet("GetOpen")]
        public IEnumerable<Order> GetOpen()
        {
            List<Order> result = new();
            List<Order> oldList = _orderService.GetAll();

            oldList.ForEach(order =>
                {
                    if (order.State == 1)
                        result.Add(order);
                });

            return result;
        }

        [HttpGet("Get")]
        public IEnumerable<Order> GetOrders()
        {
            List<Order> result = new();
            List<Order> oldList = _orderService.GetAll();

            oldList.ForEach(order =>
            {
                if (order.State != 2)
                    result.Add(order);
            });

            return result;
        }

        // GET api/<UserController>/5
        [HttpGet("Get/{id}")]
        public Order GetOrder(int id)
        {
            return _orderService.GetById(id);
        }

        // POST api/<UserController>
        [HttpPost("Create")]
        public string Add([FromBody] Order oOrder)
        {
            var result = _orderService.Create(oOrder, oOrder.User);

            if (result) return $"Заказ [{oOrder.Name}] успешно созджан !";

            return $"Заказ [{oOrder.Name}] не был создан";
        }

        // PUT api/<UserController>/5
        [HttpPut("Update/{id}")]
        public string Update([FromBody] Order oOrder)
        {
            var result = _orderService.Update(oOrder);

            if (result) return $"Заказ [{oOrder.Name}] успешно обновлен";

            return $"Заказ [{oOrder.Name}] не был обновлен(не найден)";
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete/{id}")]
        public string Remove(int id)
        {
            var result = _orderService.Delete(id);

            if (result) return "Заказ успешно удален";

            return "Заказ не был удален (не найдено)";
        }
    }
}
