using System;
using System.Collections.Generic;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IDepartmentService : IDisposable
    {
        void Create(DepartmentViewModel departmentViewModel);

        IEnumerable<DepartmentViewModel> GetAll();

        DepartmentViewModel Get(string id);

        void Update(DepartmentViewModel departmentViewModel);
    }
}
