﻿using AutoMapper;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Mappings
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<StockModel, StockViewModel>()
                .ForMember(s => s.Departments, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
