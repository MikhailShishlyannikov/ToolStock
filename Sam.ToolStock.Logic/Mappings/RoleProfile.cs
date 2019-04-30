using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleModel, RoleViewModel>()
                .ForMember(r => r.Id, opt => opt.MapFrom(rm => rm.Id))
                .ForMember(r => r.Name, opt => opt.MapFrom(rm => rm.Name));
        }
    }
}
