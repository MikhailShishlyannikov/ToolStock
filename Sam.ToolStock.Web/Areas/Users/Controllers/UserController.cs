using System;
using System.Linq;
using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Users.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IDepartmentService _departmentService;
        private readonly IStockService _stockService;

        public UserController(IUserService userService, IRoleService roleService, IDepartmentService departmentService, IStockService stockService)
        {
            _userService = userService;
            _roleService = roleService;
            _departmentService = departmentService;
            _stockService = stockService;
        }

        // GET: Users/User
        public ActionResult Show(string userId)
        {
            var user = _userService.GetUser(userId);
            user = GetRoles(user);
            user = GetDepartments(user);
            user = GetStocks(user);

            return PartialView(user);
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