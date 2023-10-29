﻿using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.ComplexTypes;
using CafeMenuMvc.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly ITokenService _tokenService;
       

        public UserManager(IMapper mapper, IEncryptionService encryptionService, ITokenService tokenService, RedisCacheManager redisCacheService)
        {
            _mapper = mapper;
            _encryptionService = encryptionService;
            _tokenService = tokenService;
            _redisCacheService = redisCacheService;
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

        public async Task<ResponseDto<UserParameter>> SignIn(UserLoginDto userDto)
        {
            var rsp = new ResponseDto<UserParameter>();
            CafeMenuEntities entity = new CafeMenuEntities();
            var usr = await entity.USER.FirstOrDefaultAsync(x => x.USERNAME == userDto.Username);
            if (usr != null)
            {
                var encrypted = _encryptionService.AESEncrypt(userDto.Password + usr.SALTPASSWORD);
                var usrpass = await entity.USER.FirstOrDefaultAsync(x => x.USERNAME == userDto.Username && x.HASHPASSWORD == encrypted);
                if (usrpass != null)
                {
                    var tokenParameters = _tokenService.GenerateToken(userDto.Username);

                    var dto = new UserParameter
                    {
                        UserId = usr.USERID,
                        Username = userDto.Username,
                        Token = tokenParameters.Item1,
                        SecretKey = tokenParameters.Item2,
                        GuidKey = Guid.NewGuid().ToString()
                    };

                    await _redisCacheService.SetAsync(dto.GuidKey, JsonConvert.SerializeObject(dto));

                    //User'a önemli bilgilerin gitmemesi için temizliyoruz.
                    dto.Username = "";
                    dto.SecretKey = "";
                    dto.Token = "";
                    dto.UserId = -1;

                    rsp.ResultStatus = ResultStatus.Success;
                    rsp.Data = dto;
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
