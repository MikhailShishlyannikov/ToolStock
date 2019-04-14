using System;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.DataProvider.Repositories;

namespace Sam.ToolStock.DataProvider.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToolContext _toolContext;
        private bool _disposed;

        public UnitOfWork(ToolContext toolContext)
        {
            _toolContext = toolContext;
        }

        public IRepository<DepartmentModel> DepartmentRepository =>
            new GenericRepository<DepartmentModel>(_toolContext);

        public IRepository<StockModel> StockRepository => new GenericRepository<StockModel>(_toolContext);

        public IRepository<ToolModel> ToolRepository => new GenericRepository<ToolModel>(_toolContext);

        public IRepository<ToolLogModel> ToolLogRepository =>
            new GenericRepository<ToolLogModel>(_toolContext);

        public IRepository<ToolTypeModel> ToolTypeRepository => new GenericRepository<ToolTypeModel>(_toolContext);

        public IRepository<UserInfoModel> UserInfoRepository =>
            new GenericRepository<UserInfoModel>(_toolContext);

        public void Save()
        {
            _toolContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _toolContext.Dispose();
            }

            _disposed = true;
        }
    }
}
