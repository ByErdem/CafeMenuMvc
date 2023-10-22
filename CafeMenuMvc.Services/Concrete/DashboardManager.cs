using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class DashboardManager:IDashboardService
    {

        public async Task<ResponseDto<CountsDto>> GetCounts()
        {
            var rsp = new ResponseDto<CountsDto>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var users = await entity.USER.ToListAsync();
            var categories = await entity.CATEGORY.ToListAsync();
            var products = await entity.PRODUCT.ToListAsync();

            rsp.Data = new CountsDto();
            rsp.Data.UserCount = users.Count;
            rsp.Data.CategoryCount = categories.Count;
            rsp.Data.ProductCount = products.Count;

            return rsp;
        }
    }
}
