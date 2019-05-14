using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
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
    }
}