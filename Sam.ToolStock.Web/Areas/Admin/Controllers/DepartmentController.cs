﻿using System.Linq;
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
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }
            _departmentService.Create(departmentViewModel);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageCreate,
                    Resources.Resource.Department,
                    departmentViewModel.Name),
                MessageType = "success",
                PageName = Resources.Resource.CreateDepartmentPage,
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
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }

            _departmentService.Update(departmentViewModel);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageUpdate,
                    Resources.Resource.Department,
                    departmentViewModel.Name),
                MessageType = "success",
                PageName = Resources.Resource.AllDepartmentsPage,
                Action = "ShowAll",
                Controller = "Department"
            };

            return View("ModalMessage", message);
        }

        public ActionResult Delete(string departmentId)
        {
            var department = _departmentService.Get(departmentId);
            if (department.Users.Any() || department.Stocks.Any())
                return RedirectToAction("Reassign", new {departmentId = department.Id});

            _departmentService.Delete(department);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageDelete,
                    Resources.Resource.Department,
                    department.Name),
                MessageType = "success",
                PageName = Resources.Resource.AllDepartmentsPage,
                Action = "ShowAll",
                Controller = "Department"
            };

            return View("ModalMessage", message);
        }

        public ActionResult Reassign(string departmentId)
        {
            var departments = _departmentService.GetAll(false).ToList();
            var department = departments.First(d => d.Id == departmentId);
            departments = departments.Where(d => d.Id != departmentId).ToList();

            var reassignViewModel = new DepartmentReassigningViewModel
            {
                DeletingDepartmentId = departmentId,
                HasUsers = department.Users.Any(),
                HasStocks = department.Stocks.Any(),
                TargetDepartments = departments
            };
            return View(reassignViewModel);
        }

        [HttpPost]
        public ActionResult Reassign(DepartmentReassigningViewModel reassignViewModel)
        {
            if (reassignViewModel.DepartmentIdForUsers == reassignViewModel.DeletingDepartmentId)
            {
                ModelState.AddModelError("DepartmentIdForUsers", Resources.Resource.YouDidNotReassignUsers);
            }
            if (reassignViewModel.DepartmentIdForStocks == reassignViewModel.DeletingDepartmentId)
            {
                ModelState.AddModelError("DepartmentIdForStocks", Resources.Resource.YouDidNotReassignStocks);
            }

            if (!ModelState.IsValid)
            {
                reassignViewModel.TargetDepartments = _departmentService.GetAll(false).ToList();
                return View(reassignViewModel);
            }

            if (reassignViewModel.HasUsers)
            {
                _departmentService.ReassignUsers(reassignViewModel.DeletingDepartmentId,
                    reassignViewModel.DepartmentIdForUsers);
            }
            if (reassignViewModel.HasStocks)
            {
                _departmentService.ReassignStocks(reassignViewModel.DeletingDepartmentId,
                    reassignViewModel.DepartmentIdForStocks);
            }

            return RedirectToAction("Delete", new { departmentId = reassignViewModel.DeletingDepartmentId });
        }

        public ActionResult Restore(string departmentId)
        {
            var dvm = _departmentService.Get(departmentId);
            dvm.IsDeleted = false;
            _departmentService.Update(dvm);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(
                    Resources.Resource.ModalPageMessageRestore,
                    Resources.Resource.Department,
                    dvm.Name),
                MessageType = "success",
                PageName = Resources.Resource.AllDepartmentsPage,
                Action = "ShowAll",
                Controller = "Department"
            };

            return View("ModalMessage", message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _departmentService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}