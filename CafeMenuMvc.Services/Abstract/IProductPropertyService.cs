using CafeMenuMvc.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Abstract
{
    public interface IProductPropertyService
    {
        Task<ResponseDto<int>> Create(ProductPropertyDto productPropertyDto);
        Task<ResponseDto<int>> Update(ProductPropertyDto productPropertyDto);
        Task<ResponseDto<int>> Delete(ProductPropertyDto productPropertyDto);
    }
}
