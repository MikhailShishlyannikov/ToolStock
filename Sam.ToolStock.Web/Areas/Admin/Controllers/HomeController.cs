using System;
using System.Linq;
using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IDepartmentService _departmentService;
        private readonly IStockService _stockService;

        public HomeController(IUserService userService, IRoleService roleService, IDepartmentService departmentService, IStockService stockService)
        {
            _userService = userService;
            _roleService = roleService;
            _departmentService = departmentService;
            _stockService = stockService;
        }

        public ActionResult Index()
        {
            var vms = _userService.GetAllTableUser().ToList();
            return View(vms);
        }

        public ActionResult ShowUser(string id)
        {
            var user = _userService.GetUser(id);
            user.Roles = _roleService.GetAll();

            var departments = _departmentService.GetAll().ToList();
            if (user.DepartmentId == new Guid().ToString())
            {
                departments.Add(new DepartmentViewModel{Id = user.DepartmentId, Name = "Choose a department"});
            }
            user.Departments = departments;

            var stocks = _stockService.GetAll().ToList();
            if (user.StockId == new Guid().ToString())
            {
                stocks.Add(new Stock { Id = user.StockId, Name = "Choose a stock" });
            }
            user.Stocks = stocks;

            return PartialView(user);
        }

        [HttpPost]
        //[MultipleButton(Name = "action", Argument = "Update")]
        public ActionResult Update(UserViewModel user)
        {
            var result = _userService.Update(user);

            if (!result) return RedirectToAction("ShowUser", new {id = user.Id});

            var message = new ModalMessageViewModel
            {
                Message = $"User {user.Id} was updated successfully!",
                MessageType = "success",
                PageName = "the users page",
                Action = "Index",
                Controller = "Home"
            };
            return PartialView("ModalMessage", message);
        }

        public ActionResult Reset(string userId)
        {
            return RedirectToAction("ShowUser", new {id = userId});
        }

        public ActionResult Delete(string userId)
        {
            var user = _userService.GetUser(userId);
            _userService.Delete(user);
            return RedirectToAction("Index");
        }
    }
}