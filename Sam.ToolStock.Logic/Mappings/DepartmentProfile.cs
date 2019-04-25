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
                .ForMember(d => d.Users, opt => opt.MapFrom(dvm => dvm.Users))
                .ForMember(d => d.Stocks, opt => opt.MapFrom(dvm => dvm.Stocks))
                .ReverseMap();
        }
    }
}
