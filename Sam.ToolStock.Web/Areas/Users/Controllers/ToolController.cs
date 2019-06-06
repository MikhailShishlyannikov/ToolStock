using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Users.Controllers
{
    [Authorize(Roles = "User")]
    public class ToolController : Controller
    {
        private readonly IToolService _toolService;
        private readonly IToolTypeService _toolTypeService;

        public ToolController(IToolService toolService, IToolTypeService toolTypeService)
        {
            _toolService = toolService;
            _toolTypeService = toolTypeService;
        }

        // GET: Users/Tool
        public ActionResult BorrowedTools(
            int page = 1,
            int pageSize = 6,
            string searchString = "",
            string manufacturer = "Choose a manufacturer",
            string toolTypeId = "Choose a tool type"
        )
        {
            var tools = _toolService.GetAllBorrowedToolCounts(false, User.Identity.GetUserId()).ToList();
            var toolTypes = _toolTypeService.GetAll().ToList();
            var manufacturers = tools.Select(t => t.Manufacturer).Distinct().ToList();


            if (searchString != "")
            {
                tools = tools.Where(t => t.Name.Contains(searchString)).ToList();
            }

            if (manufacturer != "Choose a manufacturer")
            {
                tools = tools.Where(t => t.Manufacturer == manufacturer).ToList();
            }

            if (toolTypeId != "Choose a tool type")
            {
                tools = tools.Where(t => t.ToolTypeName == toolTypes.FirstOrDefault(tt => tt.Id == toolTypeId)?.Name)
                    .ToList();
                toolTypes.Insert(0, new ToolTypeViewModel {Id = "Choose a tool type", Name = "Choose a tool type"});
            }
            else
            {
                toolTypes.Insert(0, new ToolTypeViewModel {Id = toolTypeId, Name = toolTypeId});
            }

            manufacturers.Insert(0, "Choose a manufacturer");

            var toolsPerPages = tools.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var paginationViewModel = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = tools.Count(),
                ToolCountViewModels = toolsPerPages,
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolService.Dispose();
                _toolTypeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}