using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Concrete;
using System.Collections.Generic;

namespace CafeMenuMvc.Models.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<USER, MUser>().ReverseMap();
            CreateMap<MUser, USER>().ReverseMap();

            CreateMap<PRODUCT, MProduct>().ReverseMap();
            CreateMap<MProduct, PRODUCT>().ReverseMap();

            CreateMap<CATEGORY,MCategory>().ReverseMap();
            CreateMap<MCategory, CATEGORY>().ReverseMap();

            CreateMap<List<CATEGORY>,List<MCategory>>().ReverseMap();
            CreateMap<List<MCategory>,List<CATEGORY>>().ReverseMap();
        }
    }
}