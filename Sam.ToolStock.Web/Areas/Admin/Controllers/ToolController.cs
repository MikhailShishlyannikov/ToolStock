using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class ToolController : Controller
    {
        private readonly IToolService _toolService;
        private readonly IToolTypeService _toolTypeService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;

        public ToolController(IToolService toolService, IToolTypeService toolTypeService, IStockService stockService, IUserService userService)
        {
            _toolService = toolService;
            _toolTypeService = toolTypeService;
            _stockService = stockService;
            _userService = userService;
        }

        public ActionResult Create()
        {
            var toolTypes = _toolTypeService.GetAll().ToList();
            toolTypes.Add(new ToolTypeViewModel
            {
                Id = new Guid().ToString(),
                Name = Resources.Resource.ChooseToolType
            });

            var stocks = _stockService.GetAll(false).ToList();
            stocks.Add(new StockViewModel
            {
                Id = new Guid().ToString(),
                Name = Resources.Resource.ChooseStock
            });

            var users = _userService.GetAll(false).OrderBy(u => u.FullName).ToList();
            users.Insert(0, new UserViewModel
            {
                Id = new Guid().ToString(),
                Name = Resources.Resource.ChooseUser,
                Surname = string.Empty
            });

            var tool = new ToolViewModel
            {
                Amount = 1,
                ToolTypeId = new Guid().ToString(),
                ToolTypes = toolTypes,
                StockId = new Guid().ToString(),
                Stocks = stocks,
                UserId = new Guid().ToString(),
                Users = users
            };

            return View(tool);
        }

        [HttpPost]
        public ActionResult Create(ToolViewModel toolViewModel)
        {
            if (!ModelState.IsValid)
            {
                var toolTypes = _toolTypeService.GetAll().ToList();
                toolTypes.Add(new ToolTypeViewModel
                {
                    Id = new Guid().ToString(),
                    Name = Resources.Resource.ChooseToolType
                });

                var stocks = _stockService.GetAll(false).ToList();
                stocks.Add(new StockViewModel
                {
                    Id = new Guid().ToString(),
                    Name = Resources.Resource.ChooseStock
                });

                var users = _userService.GetAll(false).OrderBy(u => u.Name).ToList();
                users.Insert(0, new UserViewModel
                {
                    Id = new Guid().ToString(),
                    Name = Resources.Resource.ChooseUser
                });

                toolViewModel.ToolTypes = toolTypes;
                toolViewModel.Stocks = stocks;
                toolViewModel.Users = users;

                return View(toolViewModel);
            }

            _toolService.Create(toolViewModel);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageCreate,
                    Resources.Resource.Tool,
                    toolViewModel.Name
                ),
                MessageType = "success",
                PageName = @Resources.Resource.CreateToolPage,
                Action = "Create",
                Controller = "Tool"
            };

            return View("ModalMessage", message);
        }

        public ActionResult ShowAll(int page = 1, int pageSize = 6)
        {

            var tools = _toolService.GetAllToolCounts().ToList();
            var toolsPerPages = tools.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var paginationViewModel = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = tools.Count(),
                ToolCountViewModels = toolsPerPages
            };

            return View(paginationViewModel);
        }
    }
}