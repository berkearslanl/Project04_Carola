using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.GalleryDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class GalleryManager : IGalleryService
    {
        private readonly IGalleryDal _GalleryDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateGalleryDto> _createValidator;
        private readonly IValidator<UpdateGalleryDto> _updateValidator;

        public GalleryManager(IGalleryDal GalleryDal, IMapper mapper, IValidator<CreateGalleryDto> createValidator, IValidator<UpdateGalleryDto> updateValidator)
        {
            _GalleryDal = GalleryDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateGalleryAsync(CreateGalleryDto createGalleryDto)
        {
            var result = _createValidator.Validate(createGalleryDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Gallery>(createGalleryDto);
            await _GalleryDal.InsertAsync(values);
        }

        public async Task DeleteGalleryAsync(int id)
        {
            await _GalleryDal.DeleteAsync(id);
        }

        public async Task<List<ResultGalleryDto>> GetAllGalleryAsync()
        {
            var values = await _GalleryDal.GetAllAsync();
            return _mapper.Map<List<ResultGalleryDto>>(values);
        }

        public async Task<GetGalleryByIdDto> GetGalleryById(int id)
        {
            var values = await _GalleryDal.GetByIdAsync(id);
            return _mapper.Map<GetGalleryByIdDto>(values);
        }

        public async Task UpdateGalleryAsync(UpdateGalleryDto updateGalleryDto)
        {
            var result = _updateValidator.Validate(updateGalleryDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<Gallery>(updateGalleryDto);
            await _GalleryDal.UpdateAsync(value);
        }
    }
}
