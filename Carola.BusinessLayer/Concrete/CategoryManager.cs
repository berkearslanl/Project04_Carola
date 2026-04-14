using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _CategoryDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;
        public CategoryManager(ICategoryDal CategoryDal, IMapper mapper, IValidator<CreateCategoryDto> createValidator, IValidator<UpdateCategoryDto> updateValidator)
        {
            _CategoryDal = CategoryDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var result = _createValidator.Validate(createCategoryDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Category>(createCategoryDto);
            await _CategoryDal.InsertAsync(values);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _CategoryDal.DeleteAsync(id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values = await _CategoryDal.GetAllAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<GetCategoryByIdDto> GetCategoryById(int id)
        {
            var values = await _CategoryDal.GetByIdAsync(id);
            return _mapper.Map<GetCategoryByIdDto>(values);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var result = _updateValidator.Validate(updateCategoryDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Category>(updateCategoryDto);
            await _CategoryDal.UpdateAsync(values);
        }
    }
}
