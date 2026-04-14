using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.DtoLayer.Dtos.SliderDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly ISliderDal _sliderDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSliderDto> _createValidator;
        private readonly IValidator<UpdateSliderDto> _updateValidator;

        public SliderManager(ISliderDal sliderDal, IMapper mapper, IValidator<CreateSliderDto> createValidator, IValidator<UpdateSliderDto> updateValidator)
        {
            _sliderDal = sliderDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateSliderAsync(CreateSliderDto createSliderDto)
        {
            var result = _createValidator.Validate(createSliderDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<Slider>(createSliderDto);
            await _sliderDal.InsertAsync(value);
        }

        public async Task DeleteSliderAsync(int id)
        {
            await _sliderDal.DeleteAsync(id);
        }

        public async Task<List<ResultSliderDto>> GetAllSliderAsync()
        {
            var values = await _sliderDal.GetAllAsync();
            return _mapper.Map<List<ResultSliderDto>>(values);
        }

        public async Task<GetSliderByIdDto> GetSliderById(int id)
        {
            var value = await _sliderDal.GetByIdAsync(id);
            return _mapper.Map<GetSliderByIdDto>(value);
        }

        public async Task UpdateSliderAsync(UpdateSliderDto updateSliderDto)
        {
            var result = _updateValidator.Validate(updateSliderDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Slider>(updateSliderDto);
            await _sliderDal.UpdateAsync(values);
        }
    }
}
