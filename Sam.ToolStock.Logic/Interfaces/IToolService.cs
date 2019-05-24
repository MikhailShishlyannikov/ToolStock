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

        IEnumerable<ToolViewModel> GetByName(string toolName);

        void IssueToUser(IssueToolViewModel toolActionsViewModel);

        void GiveInToRepair(ActionsViewModel giveInForRepairViewModel);

        void WriteOff(ActionsViewModel writeOffViewModel);

        void MoveToStock(MoveToStockViewModel moveToStockViewModel);

        void ReturnFromRepair(ActionsViewModel returnFromRepairViewModel);

        void ReturnFromUser(ReturnFromUserViewModel returnFromUserViewModel);
    }
}
