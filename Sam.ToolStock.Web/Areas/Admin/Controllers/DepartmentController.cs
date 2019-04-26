using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentViewModel departmentViewModel)
        {
            ModalMessageViewModel message;

            if (ModelState.IsValid)
            {
                _departmentService.Create(departmentViewModel);

                message = new ModalMessageViewModel
                {
                    Message = $"The department \"{departmentViewModel.Name}\" was created successfully!",
                    MessageType = "success",
                    PageName = "the create department page",
                    Action = "Create",
                    Controller = "Department"
                };

                return View("ModalMessage", message);
            }

            message = new ModalMessageViewModel
            {
                Message = $"The department \"{departmentViewModel.Name}\" wasn't created!",
                MessageType = "danger",
                PageName = "the create department page",
                Action = "Create",
                Controller = "Department"
            };
            return View("ModalMessage", message);
        }

        public ActionResult ShowAll()
        {
            var dvms = _departmentService.GetAll();
            return View(dvms);
        }

        public ActionResult Update(string departmentId)
        {
            var dvm = _departmentService.Get(departmentId);
            return View(dvm);
        }

        [HttpPost]
        public ActionResult Update(DepartmentViewModel departmentViewModel)
        {
            ModalMessageViewModel message;

            if (ModelState.IsValid)
            {
                _departmentService.Update(departmentViewModel);

                message = new ModalMessageViewModel
                {
                    Message = $"The department \"{departmentViewModel.Name}\" was updated successfully!",
                    MessageType = "success",
                    PageName = "all departments page",
                    Action = "ShowAll",
                    Controller = "Department"
                };

                return View("ModalMessage", message);
            }

            message = new ModalMessageViewModel
            {
                Message = $"The department \"{departmentViewModel.Name}\" wasn't updated!",
                MessageType = "danger",
                PageName = "all departments page",
                Action = "ShowAll",
                Controller = "Department"
            };
            return View("ModalMessage", message);
        }
    }
}