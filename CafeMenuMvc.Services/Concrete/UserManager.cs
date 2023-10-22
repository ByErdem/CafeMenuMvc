using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.ComplexTypes;
using CafeMenuMvc.Services.Abstract;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;

        public UserManager(IMapper mapper, IEncryptionService encryptionService)
        {
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        public async Task<ResponseDto<MUser>> Register(UserRegisterDto user)
        {
            var rsp = new ResponseDto<MUser>();
            CafeMenuEntities entity = new CafeMenuEntities();
            var usr = await entity.USER.FirstOrDefaultAsync(x => x.USERNAME == user.Username);
            if (usr != null)
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir kullanıcı bulunmaktadır, lütfen başka bir kullanıcı adı deneyiniz.";
                return rsp;
            }

            USER newUser = new USER
            {
                HASHPASSWORD = _encryptionService.AESEncrypt(user.Password + user.SaltPassword),
                SALTPASSWORD = user.SaltPassword,
                USERNAME = user.Username,
                NAME = user.Name,
                SURNAME = user.Surname
            };

            entity.USER.Add(newUser);
            await entity.SaveChangesAsync();

            rsp.ResultStatus = ResultStatus.Success;
            rsp.Data = _mapper.Map<MUser>(newUser);
            rsp.SuccessMessage = "Kullanıcı oluşturuldu";

            return rsp;
        }

        public async Task<ResponseDto<MUser>> SignIn(UserLoginDto userDto)
        {
            var rsp = new ResponseDto<MUser>();
            CafeMenuEntities entity = new CafeMenuEntities();
            var usr = await entity.USER.FirstOrDefaultAsync(x => x.USERNAME == userDto.Username);
            if (usr != null)
            {
                var encrypted = _encryptionService.AESEncrypt(userDto.Password + usr.SALTPASSWORD);
                var usrpass = await entity.USER.FirstOrDefaultAsync(x => x.USERNAME == userDto.Username && x.HASHPASSWORD == encrypted);
                if (usrpass != null)
                {
                    rsp.ResultStatus = ResultStatus.Success;
                    rsp.Data = _mapper.Map<MUser>(usr);
                    rsp.SuccessMessage = "Giriş başarılı";
                }
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Kullanıcı adı veya şifre yanlış!";
            }

            return rsp;
        }
    }
}
