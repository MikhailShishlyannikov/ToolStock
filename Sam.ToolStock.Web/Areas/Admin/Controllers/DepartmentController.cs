using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        //// GET: Admin/Department
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentViewModel departmentViewModel)
        {
            SuccessfulMessageViewModel message;

            if (ModelState.IsValid)
            {
                _departmentService.Create(departmentViewModel);

                message = new SuccessfulMessageViewModel
                {
                    Message = $"The department \"{departmentViewModel.Name}\" was created successfully!",
                    MessageType = "success",
                    PageName = "the create department page",
                    Action = "Create",
                    Controller = "Department"
                };

                return View("ModalMessage", message);
            }

            message = new SuccessfulMessageViewModel
            {
                Message = $"The department \"{departmentViewModel.Name}\" was created successfully!",
                MessageType = "danger",
                PageName = "the create department page",
                Action = "Create",
                Controller = "Department"
            };
            return View("ModalMessage", message);
        }

        public ActionResult ShowAllDepartments()
        {
            var dvm = _departmentService.GetAll();
            return View(dvm);
        }
    }
}