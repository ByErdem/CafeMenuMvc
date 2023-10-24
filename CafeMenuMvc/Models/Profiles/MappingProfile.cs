﻿using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using System.Collections.Generic;

namespace CafeMenuMvc.Models.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<USER, MUser>().ReverseMap();
            CreateMap<MUser, USER>().ReverseMap();

            CreateMap<PRODUCT, ProductDto>().ReverseMap();
            CreateMap<ProductDto, PRODUCT>().ReverseMap();

            CreateMap<CATEGORY,MCategory>().ReverseMap();
            CreateMap<MCategory, CATEGORY>().ReverseMap();

            CreateMap<CATEGORY, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, CATEGORY>().ReverseMap();

            CreateMap<List<CATEGORY>,List<MCategory>>().ReverseMap();
            CreateMap<List<MCategory>,List<CATEGORY>>().ReverseMap();
        }
    }
}