using AutoMapper;
using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Abstract
{
    public interface IUserService
    {
        Task<ResponseDto<MUser>> SignIn(UserLoginDto userDto);
        Task<ResponseDto<MUser>> Register(UserRegisterDto user);
    }
}
