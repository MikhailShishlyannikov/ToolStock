using System;
using System.Collections.Generic;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IToolService : IDisposable
    {
        void Create(ToolViewModel toolViewModel);

        IEnumerable<ToolViewModel> GetAll(bool showDeleted);

        IEnumerable<ToolCountViewModel> GetAllToolCounts();
    }
}
