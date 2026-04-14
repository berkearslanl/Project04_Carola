using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.BusinessLayer.ValidationRules.WhyUsValidators;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.WhyUsDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class WhyUsManager : IWhyUsService
    {
        private readonly IWhyUsDal _whyUsDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateWhyUsDto> _createValidator;
        private readonly IValidator<UpdateWhyUsDto> _updateValidator;

        public WhyUsManager(IWhyUsDal whyUsDal, IMapper mapper, IValidator<CreateWhyUsDto> createValidator, IValidator<UpdateWhyUsDto> updateValidator)
        {
            _whyUsDal = whyUsDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateWhyUsAsync(CreateWhyUsDto createWhyUsDto)
        {
            var result = _createValidator.Validate(createWhyUsDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<WhyUs>(createWhyUsDto);
            await _whyUsDal.InsertAsync(value);
        }

        public async Task DeleteWhyUsAsync(int id)
        {
            await _whyUsDal.DeleteAsync(id);
        }

        public async Task<List<ResultWhyUsDto>> GetAllWhyUsAsync()
        {
            var values = await _whyUsDal.GetAllAsync();
            return _mapper.Map<List<ResultWhyUsDto>>(values);
        }

        public async Task<GetWhyUsByIdDto> GetWhyUsById(int id)
        {
            var values = await _whyUsDal.GetByIdAsync(id);
            return _mapper.Map<GetWhyUsByIdDto>(values);
        }

        public async Task UpdateWhyUsAsync(UpdateWhyUsDto updateWhyUsDto)
        {
            var result = _updateValidator.Validate(updateWhyUsDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<WhyUs>(updateWhyUsDto);
            await _whyUsDal.UpdateAsync(value);
        }
    }
}
