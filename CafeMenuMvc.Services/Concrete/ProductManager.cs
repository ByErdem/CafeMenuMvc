using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.ComplexTypes;
using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IMapper _mapper;

        public ProductManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto<PRODUCT>> Create(MProduct productDto)
        {
            var rsp = new ResponseDto<PRODUCT>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var product = await entity.PRODUCT.FirstOrDefaultAsync(x => x.PRODUCTNAME == productDto.PRODUCTNAME);
            if (product == null)
            {
                var newProduct = _mapper.Map<PRODUCT>(product);
                entity.PRODUCT.Add(newProduct);
                await entity.SaveChangesAsync();

                rsp.Data = newProduct;
                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = "Ürün oluşturuldu.";
            }
            else
            {
                rsp.ErrorMessage = "Bu ürün sistemde tanımlı!";
                rsp.ResultStatus = ResultStatus.Error;
            }

            return rsp;
        }

        public async Task<ResponseDto<PRODUCT>> Update(MProduct productDto)
        {
            var rsp = new ResponseDto<PRODUCT>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var product = await entity.PRODUCT.FirstOrDefaultAsync(x => x.PRODUCTID == productDto.PRODUCTID);
            if (product != null)
            {
                product.PRODUCTNAME = productDto.PRODUCTNAME;
                product.PRICE = productDto.PRICE;
                product.IMAGEPATH = productDto.IMAGEPATH;

                rsp.Data = product;
                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = "Değişiklikler kaydedildi.";
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir ürün bulunamadı!";
            }

            return rsp;
        }

        public async Task<ResponseDto<int>> Delete(MProduct productDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var product = await entity.PRODUCT.FirstOrDefaultAsync(x=>x.PRODUCTID== productDto.PRODUCTID);
            if (product != null)
            {
                product.ISDELETED = true;
                entity.SaveChanges();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = $"{productDto.PRODUCTNAME} adlı ürün başarıyla silindi";
            }
            else
            {
                rsp.ResultStatus= ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir ürün bulunamadı.";
            }

            return rsp;
        }

        public async Task<ResponseDto<int>> HardDelete(MProduct productDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var product = await entity.PRODUCT.FirstOrDefaultAsync(x => x.PRODUCTID == productDto.PRODUCTID);
            if (product != null)
            {
                entity.PRODUCT.Remove(product);
                entity.SaveChanges();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = $"{productDto.PRODUCTNAME} adlı ürün başarıyla silindi";
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir ürün bulunamadı.";
            }

            return rsp;
        }
    }
}
