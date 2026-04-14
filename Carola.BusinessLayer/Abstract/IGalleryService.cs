using Carola.DtoLayer.Dtos.GalleryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface IGalleryService
    {
        Task<List<ResultGalleryDto>> GetAllGalleryAsync();
        Task<GetGalleryByIdDto> GetGalleryById(int id);
        Task CreateGalleryAsync(CreateGalleryDto createGalleryDto);
        Task UpdateGalleryAsync(UpdateGalleryDto updateGalleryDto);
        Task DeleteGalleryAsync(int id);
    }
}
