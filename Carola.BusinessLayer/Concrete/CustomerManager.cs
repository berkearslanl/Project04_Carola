using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.CustomerDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCustomerDto> _createValidator;
        private readonly IValidator<UpdateCustomerDto> _updateValidator;
        public CustomerManager(ICustomerDal customerDal, IMapper mapper, IValidator<CreateCustomerDto> createValidator, IValidator<UpdateCustomerDto> updateValidator)
        {
            _customerDal = customerDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var result = _createValidator.Validate(createCustomerDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Customer>(createCustomerDto);
            await _customerDal.InsertAsync(values);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerDal.DeleteAsync(id);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var values = await _customerDal.GetAllAsync();
            return _mapper.Map<List<ResultCustomerDto>>(values);
        }

        public async Task<GetCustomerByIdDto> GetCustomerById(int id)
        {
            var values = await _customerDal.GetByIdAsync(id);
            return _mapper.Map<GetCustomerByIdDto>(values);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var result = _updateValidator.Validate(updateCustomerDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Customer>(updateCustomerDto);
            await _customerDal.UpdateAsync(values);
        }
    }
}
