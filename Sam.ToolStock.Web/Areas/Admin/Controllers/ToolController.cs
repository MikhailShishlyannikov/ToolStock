using System;
using System.Linq;
using System.Web.Mvc;
using Sam.ToolStock.Common;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
            toolViewModel.Name = toolViewModel.Name.Trim();
            toolViewModel.Manufacturer = toolViewModel.Manufacturer.Trim();

            if (!ModelState.IsValid)
            {
                SetInToolCollections(toolViewModel);

                return View(toolViewModel);
            }

            if (toolViewModel.Status == Statuses.IssuedToUser && toolViewModel.UserId == new Guid().ToString())
            {
                ModelState.AddModelError("UserId", Resources.Resource.IfStatusIsIssuedToUserYouMustAssignUser);

                SetInToolCollections(toolViewModel);

                return View(toolViewModel);
            }

            if (toolViewModel.Status != Statuses.IssuedToUser && toolViewModel.UserId != new Guid().ToString())
            {
                ModelState.AddModelError("Status", Resources.Resource.InOrderToAssignUserYouMustSetStatusToIssuedToUser);

                SetInToolCollections(toolViewModel);

                return View(toolViewModel);
            }

            var checkedTool = _toolService.GetByName(toolViewModel.Name).FirstOrDefault();

            if (checkedTool != null && checkedTool.Manufacturer != toolViewModel.Manufacturer)
            {
                ModelState.AddModelError("Manufacturer",
                    string.Format(Resources.Resource.SystemAlreadyHasToolWithThatNameManufacturer, checkedTool.Manufacturer));

                SetInToolCollections(toolViewModel);

                return View(toolViewModel);
            }

            if (checkedTool != null && checkedTool.ToolTypeId != toolViewModel.ToolTypeId)
            {
                var toolType = _toolTypeService.Get(checkedTool.ToolTypeId);

                ModelState.AddModelError("ToolTypeId",
                    string.Format(Resources.Resource.SystemAlreadyHasToolWithThatNameManufacturer, toolType.Name));

                SetInToolCollections(toolViewModel);

                toolViewModel.ToolTypeId = toolType.Id;

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
                PageName = Resources.Resource.CreateToolPage,
                Action = "Create",
                Controller = "Tool"
            };

            return View("ModalMessage", message);
        }

        public ActionResult ShowAll(
            int page = 1, 
            int pageSize = 6, 
            string searchString = "",
            string manufacturer = null, 
            string toolTypeId = null
            )
        {
            if (manufacturer == null)
            {
                manufacturer = Resources.Resource.ChooseManufacturer;
            }
            if (toolTypeId == null)
            {
                toolTypeId = Resources.Resource.ChooseToolType;
            }

            var tools = _toolService.GetAllToolCounts(true).ToList();
            var toolTypes = _toolTypeService.GetAll().ToList();
            var manufacturers = tools.Select(t => t.Manufacturer).Distinct().ToList();


            if (searchString != "")
            {
                tools = tools.Where(t => t.Name.Contains(searchString)).ToList();
            }
            if (manufacturer != Resources.Resource.ChooseManufacturer)
            {
                tools = tools.Where(t => t.Manufacturer == manufacturer).ToList();
            }
            if (toolTypeId != Resources.Resource.ChooseToolType)
            {
                tools = tools.Where(t => t.ToolTypeName == toolTypes.FirstOrDefault(tt => tt.Id == toolTypeId)?.Name).ToList();
                toolTypes.Insert(0, new ToolTypeViewModel { Id = Resources.Resource.ChooseToolType, Name = Resources.Resource.ChooseToolType });
            }
            else
            {
                toolTypes.Insert(0, new ToolTypeViewModel { Id = toolTypeId, Name = toolTypeId });
            }

            manufacturers.Insert(0, Resources.Resource.ChooseManufacturer);

            var toolsPerPages = tools.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var paginationViewModel = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = tools.Count,
                ToolCountViewModels = toolsPerPages,
                searchString = searchString,
                Manufacturer = manufacturer,
                Manufacturers = manufacturers,
                toolTypeId = toolTypeId,
                ToolTypes = toolTypes
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("Pagination", paginationViewModel);
            }

            return View(paginationViewModel);
        }

        public ActionResult Issue(string toolName, string stockId, int maxAmount)
        {
            var users = _userService.GetAll(false).OrderBy(u => u.FullName).ToList();
            users.Insert(0, new UserViewModel { Id = new Guid().ToString(), Name = Resources.Resource.ChooseUser, Surname = "", });
            const int defaultAmount = 1;

            var issueToolViewModel = new IssueToolViewModel
            {
                ToolName = toolName,
                StockId = stockId,
                Amount = defaultAmount,
                MaxAmount = maxAmount,
                UserId = new Guid().ToString(),
                Users = users
            };

            return View(issueToolViewModel);
        }

        [HttpPost]
        public ActionResult Issue(IssueToolViewModel issueToolViewModel)
        {
            var users = _userService.GetAll(false).OrderBy(u => u.FullName).ToList();
            users.Insert(0, new UserViewModel { Id = new Guid().ToString(), Name = Resources.Resource.ChooseUser, Surname = "", });
            issueToolViewModel.Users = users;

            if (!ModelState.IsValid)
            {
                return View(issueToolViewModel);
            }

            _toolService.IssueToUser(issueToolViewModel);


            return RedirectToAction("ShowAll");
        }

        public ActionResult GiveInForRepair(string toolName, string stockId, int maxAmount)
        {
            var vm = new ActionsViewModel
            {
                ToolName = toolName,
                StockId = stockId,
                MaxAmount = maxAmount,
                Amount = 1
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult GiveInForRepair(ActionsViewModel giveInForRepairViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(giveInForRepairViewModel);
            }

            _toolService.GiveInToRepair(giveInForRepairViewModel);

            return RedirectToAction("ShowAll");
        }

        public ActionResult WriteOff(string toolName, string stockId, int maxAmount)
        {
            var vm = new ActionsViewModel
            {
                ToolName = toolName,
                StockId = stockId,
                MaxAmount = maxAmount,
                Amount = 1
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult WriteOff(ActionsViewModel writeOffViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(writeOffViewModel);
            }

            _toolService.WriteOff(writeOffViewModel);

            return RedirectToAction("ShowAll");
        }

        public ActionResult MoveToStock(string toolName, string stockId, int maxAmount)
        {
            var stocks = _stockService.GetAll(false).OrderBy(u => u.Name).ToList();
            const int defaultAmount = 1;

            var moveToStockToolViewModel = new MoveToStockViewModel()
            {
                ToolName = toolName,
                StockId = stockId,
                Amount = defaultAmount,
                MaxAmount = maxAmount,
                Stocks = stocks
            };

            return View(moveToStockToolViewModel);
        }

        [HttpPost]
        public ActionResult MoveToStock(MoveToStockViewModel moveToStockViewModel)
        {
            var stocks = _stockService.GetAll(false).OrderBy(u => u.Name).ToList();
            moveToStockViewModel.Stocks = stocks;

            if (!ModelState.IsValid)
            {
                return View(moveToStockViewModel);
            }

            _toolService.MoveToStock(moveToStockViewModel);


            return RedirectToAction("ShowAll");
        }

        public ActionResult ReturnFromRepair(string toolName, string stockId, int maxAmount)
        {
            var vm = new ActionsViewModel
            {
                ToolName = toolName,
                StockId = stockId,
                MaxAmount = maxAmount,
                Amount = 1
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult ReturnFromRepair(ActionsViewModel returnFromRepairViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(returnFromRepairViewModel);
            }

            _toolService.ReturnFromRepair(returnFromRepairViewModel);

            return RedirectToAction("ShowAll");
        }

        public ActionResult ReturnFromUser(string toolName, string stockId, int maxAmount, string userId)
        {
            var user = _userService.GetUser(userId);
            var vm = new ReturnFromUserViewModel
            {
                ToolName = toolName,
                StockId = stockId,
                MaxAmount = maxAmount,
                Amount = 1,
                UserId = userId,
                UserName = user.FullName
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult ReturnFromUser(ReturnFromUserViewModel returnFromUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(returnFromUserViewModel);
            }

            _toolService.ReturnFromUser(returnFromUserViewModel);

            return RedirectToAction("ShowAll");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userService.Dispose();
                _stockService.Dispose();
                _toolService.Dispose();
                _toolTypeService.Dispose();
            }
            base.Dispose(disposing);
        }

        private ToolViewModel SetInToolCollections(ToolViewModel toolViewModel)
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

            return toolViewModel;
        }
    }
}