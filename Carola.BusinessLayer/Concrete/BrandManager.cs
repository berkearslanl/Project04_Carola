using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;

namespace Carola.BusinessLayer.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBrandDto> _createValidator;
        private readonly IValidator<UpdateBrandDto> _updateValidator;

        public BrandManager(IBrandDal brandDal, IMapper mapper, IValidator<CreateBrandDto> createValidator, IValidator<UpdateBrandDto> updateValidator)
        {
            _brandDal = brandDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var result = _createValidator.Validate(createBrandDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Brand>(createBrandDto);
            await _brandDal.InsertAsync(values);
        }

        public async Task DeleteBrandAsync(int id)
        {
            await _brandDal.DeleteAsync(id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            var values = await _brandDal.GetAllAsync();
            return _mapper.Map<List<ResultBrandDto>>(values);
        }

        public async Task<GetBrandByIdDto> GetBrandById(int id)
        {
            var values = await _brandDal.GetByIdAsync(id);
            return _mapper.Map<GetBrandByIdDto>(values);
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var result = _updateValidator.Validate(updateBrandDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<Brand>(updateBrandDto);
            await _brandDal.UpdateAsync(value);
        }
    }
}
