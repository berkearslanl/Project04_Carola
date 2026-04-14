using Carola.DtoLayer.Dtos.CarDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DataAccessLayer.Abstract
{
    public interface ICarDal : IGenericDal<Car>
    {
        Task<List<Car>> GetAllCarsWithCategory();
        Task<List<Car>> GetLast6Cars();
        Task<List<Car>> GetCarsByPAgingAsync(int page,int pageSize);
        Task<int> GetCarCountAsync();
    }
}
