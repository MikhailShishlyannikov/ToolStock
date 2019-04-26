using System;
using System.Collections.Generic;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IDepartmentService : IDisposable
    {
        void Create(DepartmentViewModel departmentViewModel);

        IEnumerable<DepartmentViewModel> GetAll();

        IEnumerable<DepartmentViewModel> GetAll(bool ShowDeleted);

        DepartmentViewModel Get(string id);

        void Update(DepartmentViewModel departmentViewModel);

        void Delete(DepartmentViewModel departmentViewModel);

        void ReassignUsers(string deletingDepartmentId, string departmentIdForUsers);

        void ReassignStocks(string deletingDepartmentId, string departmentIdForStocks);
    }
}
