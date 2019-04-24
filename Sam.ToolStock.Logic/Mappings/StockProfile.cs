using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.Models;

namespace Sam.ToolStock.Logic.Mappings
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<StockModel, Stock>()
                .ForMember(s => s.Id, opt => opt.MapFrom(sm => sm.Id))
                .ForMember(s => s.Name, opt => opt.MapFrom(sm => sm.Name));
        }
    }
}
