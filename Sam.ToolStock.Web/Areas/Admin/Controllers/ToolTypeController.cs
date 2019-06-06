using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ToolTypeController : Controller
    {
        private readonly IToolTypeService _toolTypeService;

        public ToolTypeController(IToolTypeService toolTypeService)
        {
            _toolTypeService = toolTypeService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ToolTypeViewModel toolTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(toolTypeViewModel);
            }
            _toolTypeService.Create(toolTypeViewModel);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageCreate,
                    Resources.Resource.ToolType,
                    toolTypeViewModel.Name),
                MessageType = "success",
                PageName = Resources.Resource.CreateToolTypePage,
                Action = "Create",
                Controller = "ToolType"
            };

            return View("ModalMessage", message);
        }

        public ActionResult ShowAll()
        {
            var ttvms = _toolTypeService.GetAll();
            return View(ttvms);
        }

        public ActionResult Rename(string toolTypeId)
        {
            var toolTypeViewModel = _toolTypeService.Get(toolTypeId);

            return View(toolTypeViewModel);
        }

        [HttpPost]
        public ActionResult Rename(ToolTypeViewModel toolTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(toolTypeViewModel);
            }

            _toolTypeService.Update(toolTypeViewModel);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageUpdate,
                    Resources.Resource.ToolType,
                    toolTypeViewModel.Name),
                MessageType = "success",
                PageName = Resources.Resource.AllToolTypePage,
                Action = "ShowAll",
                Controller = "ToolType"
            };

            return View("ModalMessage", message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolTypeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}