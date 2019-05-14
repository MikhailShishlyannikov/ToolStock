using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class ToolTypeProfile : Profile
    {
        public ToolTypeProfile()
        {
            CreateMap<ToolTypeModel, ToolTypeViewModel>()
                .ReverseMap();
        }
    }
}
