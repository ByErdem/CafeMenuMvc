using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.ComplexTypes;
using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class ProductPropertyManager:IProductPropertyService
    {
        public async Task<ResponseDto<int>> Create(ProductPropertyDto productPropertyDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            string PROPERTYID = Guid.NewGuid().ToString();
            var productProperty = new PRODUCTPROPERTY
            {
                PRODUCTID = productPropertyDto.PRODUCTID,
                PROPERTYID = PROPERTYID,
            };

            var property = new PROPERTY
            {
                KEY = productPropertyDto.KEY,
                VALUE = productPropertyDto.VALUE,
                PROPERTYID = PROPERTYID
            };

            entity.PRODUCTPROPERTY.Add(productProperty);
            entity.PROPERTY.Add(property);
            await entity.SaveChangesAsync();

            rsp.ResultStatus = ResultStatus.Success;
            rsp.SuccessMessage = "Özellik eklendi";
            rsp.Data = 1;
            return rsp;
        }

        public async Task<ResponseDto<int>> Update(ProductPropertyDto productPropertyDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var property = await entity.PROPERTY.FirstOrDefaultAsync(x => x.PROPERTYID == productPropertyDto.PROPERTYID);
            if (property != null)
            {
                property.KEY = productPropertyDto.KEY;
                property.VALUE = productPropertyDto.VALUE;
                await entity.SaveChangesAsync();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.Data = 1;
                rsp.SuccessMessage = "Ürün özelliği güncellendi.";
            }
            else
            {
                rsp.ErrorMessage = "Böyle bir özellik bulunamadı.";
                rsp.ResultStatus = ResultStatus.Error;
            }

            return rsp;
        }

        public async Task<ResponseDto<int>> Delete(ProductPropertyDto productPropertyDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var property = await entity.PROPERTY.FirstOrDefaultAsync(x => x.PROPERTYID == productPropertyDto.PROPERTYID);
            if (property != null)
            {
                entity.PROPERTY.Remove(property);
                await entity.SaveChangesAsync();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.Data = 1;
                rsp.SuccessMessage = "Ürün özelliği silindi.";
            }
            else
            {
                rsp.ErrorMessage = "Böyle bir özellik bulunamadı.";
                rsp.ResultStatus = ResultStatus.Error;
            }

            return rsp;
        }

        public async Task<ResponseDto<List<PROPERTY>>> GetAllPropertiesByProductID(ProductPropertyDto productPropertyDto)
        {
            var rsp = new ResponseDto<List<PROPERTY>>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var properties = await entity.PROPERTY.Where(x => x.PROPERTYID == productPropertyDto.PROPERTYID).ToListAsync();
            if (properties.Count > 0)
            {
                rsp.ResultStatus = ResultStatus.Success;
                rsp.Data = properties;
                rsp.SuccessMessage = $"Toplam {properties.Count} özellik bulunmaktadır.";
            }
            else
            {
                rsp.ErrorMessage = "Bu ürüne tanımlı herhangi bir özellik bulunamadı.";
                rsp.ResultStatus = ResultStatus.Error;
            }

            return rsp;
        }
    }
}
