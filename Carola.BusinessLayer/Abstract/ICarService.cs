using Carola.DtoLayer.Dtos.CarDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface ICarService
    {
        Task<List<Car>> TGetAllCarsWithCategoryAsync();
        Task<List<ResultCarDto>> GetAllCarAsync();
        Task<List<ResultCarDto>> GetLast6CarAsync();
        Task<GetCarByIdDto> GetCarById(int id);
        Task CreateCarAsync(CreateCarDto createCarDto);
        Task UpdateCarAsync(UpdateCarDto updateCarDto);
        Task DeleteCarAsync(int id);

        Task<List<Car>> TGetCarsByPAgingAsync(int page, int pageSize);
        Task<int> TGetCarCountAsync();
    }
}
