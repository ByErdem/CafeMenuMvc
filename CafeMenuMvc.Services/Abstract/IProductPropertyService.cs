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
        Task<ResponseDto<int>> AddProperty(ProductPropertyDto productPropertyDto);
        Task<ResponseDto<int>> UpdateProperty(ProductPropertyDto productPropertyDto);
        Task<ResponseDto<int>> DeleteProperty(ProductPropertyDto productPropertyDto);
    }
}
