using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCarDto> _createValidator;
        private readonly IValidator<UpdateCarDto> _updateValidator;

        public CarManager(ICarDal carDal, IMapper mapper, IValidator<CreateCarDto> createValidator, IValidator<UpdateCarDto> updateValidator)
        {
            _carDal = carDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateCarAsync(CreateCarDto createCarDto)
        {
            var result = _createValidator.Validate(createCarDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<Car>(createCarDto);
            await _carDal.InsertAsync(value);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _carDal.DeleteAsync(id);
        }

        public async Task<List<ResultCarDto>> GetAllCarAsync()
        {
            var value = await _carDal.GetAllCarsWithCategory(); // Category Include ile gelir
            return _mapper.Map<List<ResultCarDto>>(value);
        }

        public async Task<GetCarByIdDto> GetCarById(int id)
        {
            var value = await _carDal.GetByIdAsync(id);
            return _mapper.Map<GetCarByIdDto>(value);
        }

        public async Task<List<ResultCarDto>> GetLast6CarAsync()
        {
            var value = await _carDal.GetLast6Cars();
            return _mapper.Map<List<ResultCarDto>>(value);
        }

        public async Task<List<Car>> TGetAllCarsWithCategoryAsync()
        {
            return await _carDal.GetAllCarsWithCategory();
        }

        public async Task<int> TGetCarCountAsync()
        {
            return await _carDal.GetCarCountAsync();
        }

        public async Task<List<Car>> TGetCarsByPAgingAsync(int page, int pageSize)
        {
            return await _carDal.GetCarsByPAgingAsync(page,pageSize);
        }

        public async Task UpdateCarAsync(UpdateCarDto updateCarDto)
        {
            var result = _updateValidator.Validate(updateCarDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<Car>(updateCarDto);
            await _carDal.UpdateAsync(value);
        }
    }
}
