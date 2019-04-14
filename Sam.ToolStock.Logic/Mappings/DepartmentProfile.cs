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
                .ReverseMap();
        }
    }
}
