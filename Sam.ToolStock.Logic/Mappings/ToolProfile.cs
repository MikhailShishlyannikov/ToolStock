using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class ToolProfile : Profile
    {
        public ToolProfile()
        {
            CreateMap<ToolModel, ToolViewModel>()
                .ReverseMap();
        }
    }
}
