using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class ToolLogProfile : Profile
    {
        public ToolLogProfile()
        {
            CreateMap<ToolLogModel, ToolLogViewModel>()
                .ReverseMap();
        }
    }
}
