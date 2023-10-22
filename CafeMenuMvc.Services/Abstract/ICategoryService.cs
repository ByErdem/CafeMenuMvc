﻿using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ResponseDto<int>> Create(MCategory categoryDto);
        Task<ResponseDto<int>> Update(MCategory categoryDto);
        Task<ResponseDto<int>> Delete(MCategory categoryDto);
        Task<ResponseDto<int>> HardDelete(MCategory categoryDto);
        Task<ResponseDto<List<MCategory>>> GetAll();
        Task<ResponseDto<List<MCategory>>> GetAllByParentID(MCategory categoryDto);
    }
}
