using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface ICategoryService 
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task<GetCategoryByIdDto> GetCategoryById(int id);
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
