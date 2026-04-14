using Carola.DtoLayer.Dtos.LocationDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface ILocationService 
    {
        Task<List<ResultLocationDto>> GetAllLocationAsync();
        Task<GetLocationByIdDto> GetLocationById(int id);
        Task CreateLocationAsync(CreateLocationDto createLocationDto);
        Task UpdateLocationAsync(UpdateLocationDto updateLocationDto);
        Task DeleteLocationAsync(int id);
    }
}
