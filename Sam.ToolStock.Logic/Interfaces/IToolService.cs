using System;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IToolService : IDisposable
    {
        void Create(ToolViewModel toolViewModel);
    }
}
