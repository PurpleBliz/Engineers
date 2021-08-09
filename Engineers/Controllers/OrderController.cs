using System;
using Microsoft.AspNetCore.Mvc;
using Engineers.ViewModels;
using Engineers.Models;
using Engineers.IService;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Engineers.Controllers
{ 
    public class OrderController : Controller
    {
        private readonly int DefaultState = 1;

        private readonly IOrderService _orderService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IFileService fileService, IUserService userService)
        {
            _orderService = orderService;
            _fileService = fileService;
            _userService = userService;
        }

        public IActionResult Index() => View(_orderService.GetAll());

        public IActionResult Create() => View();

        public IActionResult Edit(int id)
        {
            var order = _orderService.GetById(id);

            if (order == null)
                return NotFound();

            EditOrderViewModel model = new()
            {
                Id = order.Id,
                Name = order.Name,
                Description = order.Description,
                Images = order.Images.ConvertStringToList(),
                Cost = order.Cost,
                Address = order.Address,
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
                var user = _userService.GetByName(model.UserName).Result;

                if (user == null)
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");

                Order order = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Images = null,
                    Cost = model.Cost,
                    Address = model.Address,
                    State = DefaultState,
                    User = user,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now
                };

                order.Images = _fileService.UploadArray(files).ConvertListToString();

                if (_orderService.Create(order, user)) return RedirectToAction("Index");

                else return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditOrderViewModel model, IFormFileCollection files)
        {
            if (ModelState.IsValid)
            {
                Order order = _orderService.GetById(model.Id);

                order.Name = model.Name;
                order.Description = model.Description;
                order.Cost = model.Cost;
                order.Address = model.Address;
                order.State = model.State;
                order.Updated_at = DateTime.Now;

                if (files.Count != 0)
                {
                  order.Images = _fileService.UploadArray(files).ConvertListToString();
                }

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
        public IActionResult Delete(string id)
        {
            bool gg =_orderService.Delete(Convert.ToInt32(id));

            return RedirectToAction("Index");
        }
    }
}