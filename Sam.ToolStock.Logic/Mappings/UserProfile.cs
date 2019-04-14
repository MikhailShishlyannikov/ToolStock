using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterViewModel, UserModel>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(r => r.Email))
                .ForMember(u => u.Email, opt => opt.MapFrom(r => r.Email))
                .ForMember(u => u.DepartmentId, opt => opt.MapFrom(r => r.DepartmentId));

            CreateMap<RegisterViewModel, UserInfoModel>()
                .ForMember(ui => ui.Id, opt => opt.Ignore())
                .ForMember(ui => ui.Name, opt => opt.MapFrom(r => r.Name))
                .ForMember(ui => ui.Patronymic, opt => opt.MapFrom(r => r.Patronymic))
                .ForMember(ui => ui.Surname, opt => opt.MapFrom(r => r.Surname))
                .ForMember(ui => ui.Email, opt => opt.MapFrom(r => r.Email))
                .ForMember(ui => ui.Phone, opt => opt.MapFrom(r => r.Phone));
        }
    }
}
