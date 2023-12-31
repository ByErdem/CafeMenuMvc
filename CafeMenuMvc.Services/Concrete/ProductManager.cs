﻿using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.ComplexTypes;
using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CafeMenuMvc.Services.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public ProductManager(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;

        }

        public async Task<ResponseDto<ProductDto>> Create(ProductDto productDto, HttpContext context)
        {
            var rsp = new ResponseDto<ProductDto>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var product = await entity.PRODUCT.FirstOrDefaultAsync(x => x.PRODUCTNAME == productDto.PRODUCTNAME);
            if (product == null)
            {
                byte[] imageBytes = Convert.FromBase64String(productDto.IMAGEBASE64);

                var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                var imageName = Guid.NewGuid().ToString() + ".png";
                string filePath = context.Server.MapPath("~/Images/") + imageName;
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                var newProduct = _mapper.Map<PRODUCT>(productDto);
                newProduct.IMAGEPATH = imageName;
                newProduct.CREATEDDATE = DateTime.Now;
                newProduct.ISDELETED = false;

                entity.PRODUCT.Add(newProduct);
                await entity.SaveChangesAsync();

                productDto = _mapper.Map<ProductDto>(newProduct);
                productDto.CATEGORYNAME = await _categoryService.FindParentCategoryName(productDto.CATEGORYID);

                rsp.Data = productDto;
                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = "Ürün oluşturuldu.";
            }
            else
            {

                if (product.ISDELETED.Value)
                {
                    var newProductDto = _mapper.Map<ProductDto>(product);

                    product.ISDELETED = false;
                    newProductDto.ISDELETED = false;

                    if (productDto.CATEGORYID > 0)
                    {
                        product.CATEGORYID = productDto.CATEGORYID;
                        newProductDto.CATEGORYID = productDto.CATEGORYID;
                        newProductDto.CATEGORYNAME = await _categoryService.FindParentCategoryName(productDto.CATEGORYID);
                    }

                    await entity.SaveChangesAsync();

                    rsp.ResultStatus = ResultStatus.Success;
                    rsp.SuccessMessage = "Kategori başarıyla eklendi.";
                    rsp.Data = newProductDto;
                }
                else
                {
                    rsp.ErrorMessage = $"{productDto.PRODUCTNAME } isimli ürün sistemde tanımlıdır!";
                    rsp.ResultStatus = ResultStatus.Error;
                    rsp.Data = _mapper.Map<ProductDto>(product);
                }
            }

            return rsp;
        }

        public async Task<ResponseDto<PRODUCT>> Update(ProductDto productDto)
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

        public async Task<ResponseDto<int>> Delete(ProductDto productDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var product = await entity.PRODUCT.FirstOrDefaultAsync(x => x.PRODUCTID == productDto.PRODUCTID);
            if (product != null)
            {
                product.ISDELETED = true;
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

        public async Task<ResponseDto<int>> HardDelete(ProductDto productDto)
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

        public async Task<ResponseDto<List<ProductDto>>> GetAll()
        {
            var rsp = new ResponseDto<List<ProductDto>>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var products = await entity.PRODUCT.Where(x => x.ISDELETED == false).ToListAsync();

            if (products.Count > 0)
            {
                rsp.Data = _mapper.Map<List<ProductDto>>(products);

                for (int i = 0; i < rsp.Data.Count; i++)
                {
                    if (rsp.Data[i].CATEGORYID > 0)
                    {
                        rsp.Data[i].CATEGORYNAME = await _categoryService.FindParentCategoryName(rsp.Data[i].CATEGORYID);
                    }
                }

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = $"Toplamda {products.Count} adet kategori listelendi";
            }
            else
            {
                rsp.Data = new List<ProductDto>();
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Sistemde tanımlı bir kategori yok.";
            }

            return rsp;


        }
    }
}
