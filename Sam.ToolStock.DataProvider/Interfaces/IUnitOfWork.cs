using System;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<DepartmentModel> DepartmentRepository { get; }
        IRepository<StockModel> StockRepository { get; }
        IRepository<ToolModel> ToolRepository { get; }
        IRepository<ToolLogModel> ToolLogRepository { get; }
        IRepository<ToolTypeModel> ToolTypeRepository { get; }
        IRepository<UserInfoModel> UserInfoRepository { get; }

        void Save();
    }
}
