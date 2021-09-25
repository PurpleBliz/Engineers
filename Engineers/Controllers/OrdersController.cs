using System;
using Microsoft.AspNetCore.Mvc;
using Engineers.ViewModels;
using Engineers.Models;
using Engineers.IService;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Engineers.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrdersController : Controller
    {
        private readonly int DefaultState = 1;

        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrdersController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Index() => View((List<Order>)_orderService.GetAll().Data);

        public IActionResult CreateWithBlueprint() => View((List<Blueprint>)_orderService.GetBlueprints().Data);

        public IActionResult Create() => View();

        public IActionResult SendRespond(int id)
        {
            CreateRespondViewModel model = new()
            {
                UserId = "",
                OrderId = id
            };

            return View(model);
        }

        public IActionResult GetResponds(int id) => View((List<Respond>)_orderService.GetResponds(id).Data);

        public IActionResult Edit(int id)
        {
            var order = (Order)_orderService.GetById(id).Data;

            if (order == null)
                return NotFound();

            EditOrderViewModel model = new()
            {
                Id = order.Id,
                Name = order.Name,
                Description = order.Description,
                Images = order.Images.ConvertStringToList(),
                Cost = order.Cost,
                Longitude =order.Longitude,
                Latitude = order.Latitude,
                State = order.State,
                Updated_at = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model, IFormFileCollection files)
        {
            if (ModelState.IsValid)
            {
                Order order = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Images = null,
                    Cost = model.Cost,
                    State = DefaultState,
                    Owner = (User)_userService.GetByName(model.UserName).Data,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now
                };

                order.Images = _orderService.UploadImage(files).Data.ToString();

                if (_orderService.Create(order).Success) return RedirectToAction("Index");

                else return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult SendRespond(CreateRespondViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (User)_userService.GetByName(model.UserName).Data;

                Respond respond = new()
                {
                    OrderId = model.OrderId,
                    UserId = userId.Id,
                    Text = model.Text,
                    Created_at = DateTime.Now
                };

                if (_orderService.SendRespond(respond).Success) return RedirectToAction("Index");

                else return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateWithBlueprint(string id)
        {
            List<Blueprint> models = (List<Blueprint>)_orderService.GetBlueprints().Data;

            Blueprint model = models.FirstOrDefault(print => print.Name == id);

            if (model == null) return RedirectToAction("CreateWithBlueprint");

            Order order = new()
            {
                Name = model.Name,
                Description = model.Description,
                Images = Properties.PathToDefaultOrderImage,
                Cost = model.Cost,
                State = DefaultState,
                Owner = (User)_userService.GetByName("admin").Data,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            if (_orderService.Create(order).Success) return RedirectToAction("Index");

            else return RedirectToAction("CreateWithBlueprint");
        }

        [HttpPost]
        public IActionResult Edit(EditOrderViewModel model, IFormFileCollection files)
        {
            if (ModelState.IsValid)
            {
                Order order = (Order)_orderService.GetById(model.Id).Data;

                order.Name = model.Name;
                order.Description = model.Description;
                order.Cost = model.Cost;
                order.Longitude = model.Longitude;
                order.Latitude = model.Latitude;
                order.State = model.State;
                order.Updated_at = DateTime.Now;

                order.Images = _orderService.UploadImage(files).Data.ToString();

                try
                {
                    _orderService.Update(order);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveRespond(int id)
        {
            _orderService.RemoveRespond(id);

            return RedirectToAction("Index");
        }
    }
}