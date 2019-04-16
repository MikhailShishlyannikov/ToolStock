using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterViewModel, UserModel>()
                .ForMember(um => um.UserName, opt => opt.MapFrom(r => r.Email))
                .ForMember(um => um.Email, opt => opt.MapFrom(r => r.Email))
                .ForMember(um => um.DepartmentId, opt => opt.MapFrom(r => r.DepartmentId));

            CreateMap<RegisterViewModel, UserInfoModel>()
                .ForMember(ui => ui.Id, opt => opt.Ignore())
                .ForMember(ui => ui.Name, opt => opt.MapFrom(r => r.Name))
                .ForMember(ui => ui.Patronymic, opt => opt.MapFrom(r => r.Patronymic))
                .ForMember(ui => ui.Surname, opt => opt.MapFrom(r => r.Surname))
                .ForMember(ui => ui.Email, opt => opt.MapFrom(r => r.Email))
                .ForMember(ui => ui.Phone, opt => opt.MapFrom(r => r.Phone));

            CreateMap<UserModel, User>()
                .ForMember(u => u.Id, opt => opt.MapFrom(um => um.Id));

            CreateMap<UserInfoModel, ProfileViewModel>()
                .ForMember(pvm => pvm.Name, opt => opt.MapFrom(ui => ui.Name))
                .ForMember(pvm => pvm.Patronymic, opt => opt.MapFrom(ui => ui.Patronymic))
                .ForMember(pvm => pvm.Surname, opt => opt.MapFrom(ui => ui.Surname));
        }
    }
}
