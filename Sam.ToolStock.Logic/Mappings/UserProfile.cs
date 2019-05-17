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
            
            CreateMap<UserInfoModel, ProfileViewModel>()
                .ForMember(pvm => pvm.Name, opt => opt.MapFrom(ui => ui.Name))
                .ForMember(pvm => pvm.Patronymic, opt => opt.MapFrom(ui => ui.Patronymic))
                .ForMember(pvm => pvm.Surname, opt => opt.MapFrom(ui => ui.Surname));


            CreateMap<UserInfoModel, TableUserViewModel>()
                .ForMember(tu => tu.Id, opt => opt.MapFrom(ui => ui.User.Id))
                .ForMember(tu => tu.FullName,
                    opt => opt.MapFrom(ui =>
                        ui.Patronymic == null
                            ? $"{ui.Name} {ui.Surname}"
                            : $"{ui.Name} {ui.Patronymic} {ui.Surname}"))
                .ForMember(tu => tu.Department,
                    opt => opt.MapFrom(ui => ui.User.Department == null ? "" : ui.User.Department.Name))
                .ForMember(tu => tu.Stock, opt => opt.MapFrom(ui => ui.User.Stock == null ? "" : ui.User.Stock.Name));

            CreateMap<UserInfoModel, UserViewModel>()
                .ForMember(u => u.Id, opt => opt.MapFrom(ui => ui.User.Id))
                .ForMember(u => u.Name, opt => opt.MapFrom(ui => ui.Name))
                .ForMember(u => u.Patronymic, opt => opt.MapFrom(ui => ui.Patronymic))
                .ForMember(u => u.Surname, opt => opt.MapFrom(ui => ui.Surname))
                .ForMember(u => u.Email, opt => opt.MapFrom(ui => ui.Email))
                .ForMember(u => u.Phone, opt => opt.MapFrom(ui => ui.Phone))
                .ForMember(u => u.DepartmentId, opt => opt.MapFrom(ui => ui.User.DepartmentId))
                .ForMember(u => u.StockId, opt => opt.MapFrom(ui => ui.User.StockId));

            CreateMap<UserViewModel, UserInfoModel>()
                .ForMember(ui => ui.Name, opt => opt.MapFrom(u => u.Name))
                .ForMember(ui => ui.Patronymic, opt => opt.MapFrom(u => u.Patronymic))
                .ForMember(ui => ui.Surname, opt => opt.MapFrom(u => u.Surname))
                .ForMember(ui => ui.Phone, opt => opt.MapFrom(u => u.Phone))
                .ForMember(ui => ui.IsDeleted, opt => opt.MapFrom(u => u.IsDeleted));

            CreateMap<UserViewModel, UserModel>()
                .ForMember(um => um.DepartmentId, opt => opt.MapFrom(u => u.DepartmentId))
                .ForMember(um => um.StockId, opt => opt.MapFrom(u => u.StockId))
                .ForMember(um => um.IsDeleted, opt => opt.MapFrom(u => u.IsDeleted));

            CreateMap<UserModel, UserViewModel>()
                .ForMember(u => u.Id, opt => opt.MapFrom(um => um.Id))
                .ForMember(u => u.Name, opt => opt.MapFrom(um => um.UserInfo.Name))
                .ForMember(u => u.Patronymic, opt => opt.MapFrom(um => um.UserInfo.Patronymic))
                .ForMember(u => u.Surname, opt => opt.MapFrom(um => um.UserInfo.Surname));
        }
    }
}
