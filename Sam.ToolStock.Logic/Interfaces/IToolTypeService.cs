using System;
using System.Collections.Generic;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IToolTypeService : IDisposable
    {
        void Create(ToolTypeViewModel toolTypeViewModel);

        IEnumerable<ToolTypeViewModel> GetAll();

        ToolTypeViewModel Get(string id);

        void Update(ToolTypeViewModel toolTypeViewModel);
    }
}
