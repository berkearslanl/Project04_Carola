using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.LocationDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class LocationManager : ILocationService
    {
        private readonly ILocationDal _LocationDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateLocationDto> _createValidator;
        private readonly IValidator<UpdateLocationDto> _updateValidator;
        public LocationManager(ILocationDal LocationDal, IMapper mapper, IValidator<CreateLocationDto> createValidator, IValidator<UpdateLocationDto> updateValidator)
        {
            _LocationDal = LocationDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateLocationAsync(CreateLocationDto createLocationDto)
        {
            var result = _createValidator.Validate(createLocationDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Location>(createLocationDto);
            await _LocationDal.InsertAsync(values);
        }

        public async Task DeleteLocationAsync(int id)
        {
            await _LocationDal.DeleteAsync(id);
        }

        public async Task<List<ResultLocationDto>> GetAllLocationAsync()
        {
            var values = await _LocationDal.GetAllAsync();
            return _mapper.Map<List<ResultLocationDto>>(values);
        }

        public async Task<GetLocationByIdDto> GetLocationById(int id)
        {
            var values = await _LocationDal.GetByIdAsync(id);
            return _mapper.Map<GetLocationByIdDto>(values);
        }

        public async Task UpdateLocationAsync(UpdateLocationDto updateLocationDto)
        {
            var result = _updateValidator.Validate(updateLocationDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Location>(updateLocationDto);
            await _LocationDal.UpdateAsync(values);
        }
    }
}
