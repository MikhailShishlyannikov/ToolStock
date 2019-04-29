using System.Linq;
using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, DepartmentModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(dvm => dvm.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(dvm => dvm.Name))
                .ForMember(d => d.IsDeleted, opt => opt.MapFrom(dvm => dvm.IsDeleted))
                .ForMember(d => d.Users, opt => opt.Ignore())
                .ForMember(d => d.Stocks, opt => opt.Ignore());

            CreateMap<DepartmentModel, DepartmentViewModel>()
                .ForMember(dvm => dvm.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(dvm => dvm.Name, opt => opt.MapFrom(d => d.Name))
                .ForMember(dvm => dvm.IsDeleted, opt => opt.MapFrom(d => d.IsDeleted))
                .ForMember(dvm => dvm.Users, opt => opt.MapFrom(d => d.Users.Where(u => u.IsDeleted != true)))
                .ForMember(dvm => dvm.Stocks, opt => opt.MapFrom(d => d.Stocks.Where((s => s.IsDeleted != true))));
        }
    }
}
