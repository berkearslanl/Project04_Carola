using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.VideoDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;

namespace Carola.BusinessLayer.Concrete
{
    public class VideoManager : IVideoService
    {
        private readonly IVideoDal _videoDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateVideoDto> _createValidator;
        private readonly IValidator<UpdateVideoDto> _updateValidator;

        public VideoManager(IVideoDal videoDal, IMapper mapper, IValidator<CreateVideoDto> createValidator, IValidator<UpdateVideoDto> updateValidator)
        {
            _videoDal = videoDal;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task CreateVideoAsync(CreateVideoDto createVideoDto)
        {
            var result = _createValidator.Validate(createVideoDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<Video>(createVideoDto);
            await _videoDal.InsertAsync(value);
        }

        public async Task DeleteVideoAsync(int id)
        {
            await _videoDal.DeleteAsync(id);
        }

        public async Task<List<ResultVideoDto>> GetAllVideoAsync()
        {
            var values = await _videoDal.GetAllAsync();
            return _mapper.Map<List<ResultVideoDto>>(values);
        }

        public async Task<GetVideoByIdDto> GetVideoById(int id)
        {
            var value = await _videoDal.GetByIdAsync(id);
            return _mapper.Map<GetVideoByIdDto>(value);
        }

        public async Task UpdateVideoAsync(UpdateVideoDto updateVideoDto)
        {
            var result = _updateValidator.Validate(updateVideoDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var value = _mapper.Map<Video>(updateVideoDto);
            await _videoDal.UpdateAsync(value);
        }
    }
}
