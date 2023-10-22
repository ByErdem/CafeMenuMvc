using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Abstract
{
    public interface IProductService
    {
        Task<ResponseDto<MProduct>> Create(MProduct productDto);
    }
}
