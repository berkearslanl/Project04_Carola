using Carola.DtoLayer.Dtos.VideoDtos;

namespace Carola.BusinessLayer.Abstract
{
    public interface IVideoService
    {
        Task<List<ResultVideoDto>> GetAllVideoAsync();
        Task<GetVideoByIdDto> GetVideoById(int id);
        Task CreateVideoAsync(CreateVideoDto createVideoDto);
        Task UpdateVideoAsync(UpdateVideoDto updateVideoDto);
        Task DeleteVideoAsync(int id);
    }
}
