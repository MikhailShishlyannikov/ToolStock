using System;
using System.Linq;
using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IDepartmentService _departmentService;

        public StockController(IStockService stockService, IDepartmentService departmentService)
        {
            _stockService = stockService;
            _departmentService = departmentService;
        }

        public ActionResult Create()
        {
            var departments = _departmentService.GetAll(false).ToList();
            departments.Add(new DepartmentViewModel
            {
                Id = new Guid().ToString(),
                Name = Resources.Resource.ChooseDepartment
            });

            var stock = new StockViewModel
            {
                DepartmentId = new Guid().ToString(),
                Departments = departments
            };

            return View(stock);
        }

        [HttpPost]
        public ActionResult Create(StockViewModel stockViewModel)
        {
            if (stockViewModel.DepartmentId == new Guid().ToString())
            {
                ModelState.AddModelError("DepartmentId", Resources.Resource.YouDidNotChooseDepartment);
            }

            if (!ModelState.IsValid)
            {
                var departments = _departmentService.GetAll(false).ToList();
                departments.Add(new DepartmentViewModel
                {
                    Id = new Guid().ToString(),
                    Name = Resources.Resource.ChooseDepartment
                });

                stockViewModel.DepartmentId = new Guid().ToString();
                stockViewModel.Departments = departments;

                return View(stockViewModel);
            }

            _stockService.Create(stockViewModel);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageCreate,
                    Resources.Resource.Stock,
                    stockViewModel.Name
                ),
                MessageType = "success",
                PageName = @Resources.Resource.CreateStockPage,
                Action = "Create",
                Controller = "Stock"
            };

            return View("ModalMessage", message);
        }

        public ActionResult ShowAll()
        {
            var stockViewModels = _stockService.GetAll();
            return View(stockViewModels);
        }

        public ActionResult Update(string stockId)
        {
            var stockViewModel = _stockService.Get(stockId);
            var departments = _departmentService.GetAll();
            stockViewModel.Departments = departments;

            return View(stockViewModel);
        }

        [HttpPost]
        public ActionResult Update(StockViewModel stockViewModel)
        {
            ModalMessageViewModel message;

            if (!ModelState.IsValid)
            {
                var departments = _departmentService.GetAll();
                stockViewModel.Departments = departments;

                return View(stockViewModel);
            }

            _stockService.Update(stockViewModel);

            message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageUpdate,
                    Resources.Resource.Stock,
                    stockViewModel.Name
                ),
                MessageType = "success",
                PageName = Resources.Resource.AllStocksPage,
                Action = "ShowAll",
                Controller = "Stock"
            };

            return View("ModalMessage", message);
        }

        public ActionResult Delete(string stockId)
        {
            var stock = _stockService.Get(stockId);

            if (stock.Users.Any() || stock.Tools.Any())
                return RedirectToAction("Reassign", new {stockId = stock.Id});

            _stockService.Delete(stock);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageDelete,
                    Resources.Resource.Stock,
                    stock.Name
                ),
                MessageType = "success",
                PageName = Resources.Resource.AllStocksPage,
                Action = "ShowAll",
                Controller = "Stock"
            };

            return View("ModalMessage", message);
        }

        public ActionResult Reassign(string stockId)
        {
            var stocks = _stockService.GetAll(false).ToList();
            var stock = stocks.First(d => d.Id == stockId);

            var reassignViewModel = new StockReassigningViewModel
            {
                DeletingStockId = stockId,
                HasUsers = stock.Users.Any(),
                HasTools = stock.Tools.Any(),
                TargetStocks = stocks
            };
            return View(reassignViewModel);
        }

        [HttpPost]
        public ActionResult Reassign(StockReassigningViewModel reassignViewModel)
        {
            if (reassignViewModel.StockIdForUsers == reassignViewModel.DeletingStockId)
            {
                ModelState.AddModelError("StockIdForUsers", Resources.Resource.YouDidNotReassignUsers);
            }
            if (reassignViewModel.StockIdForTools == reassignViewModel.DeletingStockId)
            {
                ModelState.AddModelError("StockIdForTools", Resources.Resource.YouDidNotReassignTools);
            }

            if (!ModelState.IsValid)
            {
                reassignViewModel.TargetStocks = _stockService.GetAll(false).ToList();
                return View(reassignViewModel);
            }

            if (reassignViewModel.HasUsers)
            {
                _stockService.ReassignUsers(reassignViewModel.DeletingStockId,
                    reassignViewModel.StockIdForUsers);
            }
            if (reassignViewModel.HasTools)
            {
                _stockService.ReassignTools(reassignViewModel.DeletingStockId,
                    reassignViewModel.StockIdForTools);
            }
            

            return RedirectToAction("Delete", new { stockId = reassignViewModel.DeletingStockId });
        }

        public ActionResult Restore(string stockId)
        {
            var stockViewModel = _stockService.Get(stockId);
            stockViewModel.IsDeleted = false;
            _stockService.Update(stockViewModel);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageRestore,
                    Resources.Resource.Stock,
                    stockViewModel.Name
                ),
                MessageType = "success",
                PageName = Resources.Resource.AllStocksPage,
                Action = "ShowAll",
                Controller = "Stock"
            };

            return View("ModalMessage", message);
        }
    }
}