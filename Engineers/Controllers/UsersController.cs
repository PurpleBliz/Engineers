using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Engineers.Models;
using Engineers.ViewModels;
using Microsoft.AspNetCore.Http;
using Engineers.IService;
using Microsoft.AspNetCore.Authorization;

namespace Engineers.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;

        public UsersController(UserManager<User> userManager, IFileService fileService)
        {
            _userManager = userManager;
            _fileService = fileService;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult GetCustomer()
        {
            ViewBag.Role = Roles.CUSTOMER_RU;
            return View("GetByRole", _userManager.Users.Where(user => user.Role == Roles.CUSTOMER_EN));
        }

        public IActionResult GetExecutor()
        {
            ViewBag.Role = Roles.EXECUTOR_RU;
            return View("GetByRole", _userManager.Users.Where(user => user.Role == Roles.EXECUTOR_EN));
        }

        public IActionResult Create(string id)
        {
            CreateUserViewModel model = new();

            model.Role = id;

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new() 
            { 
                Id = user.Id, 
                PhoneNumber = user.PhoneNumber,
                MinDescription = user.MinDescription,
                Fulldescription = user.Fulldescription,
                City = user.City,
                Age = user.Age,
                Education = user.Education,
                Balance = user.Balance,
                UserName = user.UserName,
                Image = user.Image,
                Role = user.Role,
                FullName = user.FullName
            };

            return View(model);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new() { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                User user = model.ConverToUser();

                if (file != null)
                {
                    var filePath = _fileService.Upload(file);

                    user.Image = filePath;
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (user.Role == "customer")
                        return RedirectToAction("GetCustomer");
                    else
                        return RedirectToAction("GetExecutor");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    var result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                var _role = user.Role;

                if (user != null)
                {
                    if (file != null)
                    {
                        if (user.Image == Properties.PathToDefaultUserImage || user.Image == Properties.PathToDefaultOrderImage)
                            model.Image = _fileService.Upload(file);
                        else if (_fileService.Delete(user.Image)) model.Image = _fileService.Upload(file);
                    }

                    user.UpdateInfo(model);

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        if (_role == "customer")
                            return RedirectToAction("GetCustomer");
                        else
                            return RedirectToAction("GetExecutor");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var _role = user.Role;

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            if (_role == "customer")
                return RedirectToAction("GetCustomer");
            else
                return RedirectToAction("GetExecutor");
        }
    }
}
