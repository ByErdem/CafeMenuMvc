using AutoMapper;
using CafeMenuMvc.Data;
using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.ComplexTypes;
using CafeMenuMvc.Services.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> Create(MCategory categoryDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var category = await entity.CATEGORY.FirstOrDefaultAsync(x => x.CATEGORYNAME == categoryDto.CATEGORYNAME);
            if (category == null)
            {
                var newCategory = _mapper.Map<CATEGORY>(categoryDto);
                entity.CATEGORY.Add(newCategory);
                await entity.SaveChangesAsync();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = "Kategori başarıyla eklendi.";
                rsp.Data = 1;
            }
            else
            {
                rsp.ErrorMessage = $"{categoryDto.CATEGORYNAME} isimli kategori sistemde tanımlıdır.";
                rsp.ResultStatus = ResultStatus.Error;
                rsp.Data = 0;
            }

            return rsp;
        }

        public async Task<ResponseDto<int>> Update(MCategory categoryDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var category = await entity.CATEGORY.FirstOrDefaultAsync(x => x.CATEGORYID == categoryDto.CATEGORYID);
            if (category != null)
            {
                category.CATEGORYNAME = categoryDto.CATEGORYNAME;
                await entity.SaveChangesAsync();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = "Değişiklikler başarıyla kaydedildi.";
                rsp.Data = 1;
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir kategori bulunamadı.";
            }

            return rsp;
        }

        public async Task<ResponseDto<int>> Delete(MCategory categoryDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var category = await entity.CATEGORY.FirstOrDefaultAsync(x => x.CATEGORYID == categoryDto.CATEGORYID);
            if (category != null)
            {
                category.ISDELETED = true;
                await entity.SaveChangesAsync();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = $"{categoryDto.CATEGORYNAME} isimli kategori başarıyla silindi.";
                rsp.Data = 1;
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir kategori bulunamadı.";
            }

            return rsp;
        }

        public async Task<ResponseDto<int>> HardDelete(MCategory categoryDto)
        {
            var rsp = new ResponseDto<int>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var category = await entity.CATEGORY.FirstOrDefaultAsync(x => x.CATEGORYID == categoryDto.CATEGORYID);
            if (category != null)
            {
                entity.CATEGORY.Remove(category);
                await entity.SaveChangesAsync();

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = $"{categoryDto.CATEGORYNAME} isimli kategori sistemden başarıyla silindi.";
                rsp.Data = 1;
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir kategori bulunamadı.";
            }

            return rsp;
        }


        public async Task<ResponseDto<CategoryDto>> Get(CategoryDto dto)
        {
            var rsp = new ResponseDto<CategoryDto>();
            CafeMenuEntities entity = new CafeMenuEntities();
            var category = await entity.CATEGORY.FirstOrDefaultAsync(x => x.CATEGORYID == dto.CATEGORYID);
            if (category != null)
            {
                rsp.Data = _mapper.Map<CategoryDto>(category);
                rsp.ResultStatus= ResultStatus.Success;
                rsp.SuccessMessage = "Kategori kaydı alındı";
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Böyle bir kayıt bulunamadı";
            }

            return rsp;
        }


        public async Task<ResponseDto<List<CategoryDto>>> GetAll()
        {
            var rsp = new ResponseDto<List<CategoryDto>>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var categories = await entity.CATEGORY.ToListAsync();

            if (categories.Count > 0)
            {
                rsp.Data = _mapper.Map<List<CategoryDto>>(categories);

                for (int i = 0; i < rsp.Data.Count; i++)
                {
                    if (rsp.Data[i].PARENTCATEGORYID != 0)
                    {
                        var categoryDto = new CategoryDto {
                            CATEGORYID = rsp.Data[i].PARENTCATEGORYID
                        };

                        var foundCategory = await Get(categoryDto);
                        rsp.Data[i].PARENTCATEGORYNAME = foundCategory.Data.CATEGORYNAME;
                    }
                }

                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = $"Toplamda {categories.Count} adet kategori listelendi";
            }
            else
            {
                rsp.Data = new List<CategoryDto>();
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Sistemde tanımlı bir kategori yok.";
            }

            return rsp;
        }

        public async Task<ResponseDto<List<MCategory>>> GetAllByParentID(MCategory categoryDto)
        {
            var rsp = new ResponseDto<List<MCategory>>();
            CafeMenuEntities entity = new CafeMenuEntities();

            var categories = await entity.CATEGORY.Where(x => x.PARENTCATEGORYID == categoryDto.PARENTCATEGORYID).ToListAsync();

            if (categories.Count > 0)
            {
                rsp.Data = _mapper.Map<List<MCategory>>(categories);
                rsp.ResultStatus = ResultStatus.Success;
                rsp.SuccessMessage = $"Toplamda {categories.Count} adet kategori listelendi";
            }
            else
            {
                rsp.ResultStatus = ResultStatus.Error;
                rsp.ErrorMessage = "Sistemde tanımlı bir kategori yok.";
            }

            return rsp;
        }
    }
}
