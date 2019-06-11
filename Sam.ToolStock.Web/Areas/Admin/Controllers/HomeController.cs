using System;
using System.Linq;
using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
            user = GetRoles(user);
            user = GetDepartments(user);
            user = GetStocks(user);

            return PartialView(user);
        }

        [HttpPost]
        public ActionResult Update(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                user = GetRoles(user);
                user = GetDepartments(user);
                user = GetStocks(user);

                return PartialView("ShowUser", user);
            }

            var result = _userService.Update(user);

            if (!result) return RedirectToAction("ShowUser", new {id = user.Id});

            var message = new ModalMessageViewModel
            {
                Message = string.Format(Resources.Resource.ModalPageMessageUpdate, Resources.Resource.User, user.Id),
                MessageType = "success",
                PageName = Resources.Resource.UsersPage,
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _departmentService.Dispose();
                _userService.Dispose();
                _roleService.Dispose();
                _stockService.Dispose();
            }
            base.Dispose(disposing);
        }

        private UserViewModel GetRoles(UserViewModel user)
        {
            user.Roles = _roleService.GetAll();
            return user;
        }

        private UserViewModel GetDepartments(UserViewModel user)
        {
            var departments = _departmentService.GetAll().ToList();
            if (user.DepartmentId == new Guid().ToString())
            {
                departments.Add(new DepartmentViewModel { Id = user.DepartmentId, Name = Resources.Resource.ChooseDepartment });
            }
            user.Departments = departments;

            return user;
        }

        private UserViewModel GetStocks(UserViewModel user)
        {
            var stocks = _stockService.GetAll().ToList();
            if (user.StockId == new Guid().ToString())
            {
                stocks.Add(new StockViewModel { Id = user.StockId, Name = Resources.Resource.ChooseStock });
            }
            user.Stocks = stocks;

            return user;
        }
    }
}