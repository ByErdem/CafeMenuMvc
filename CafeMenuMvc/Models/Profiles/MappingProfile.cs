using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Concrete;

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
        }
    }
}