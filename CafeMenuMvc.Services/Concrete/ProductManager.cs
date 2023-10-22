using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class ProductManager : IProductService
    {
        public Task<ResponseDto<MProduct>> Create(MProduct productDto)
        {
            return null;
        }
    }
}
