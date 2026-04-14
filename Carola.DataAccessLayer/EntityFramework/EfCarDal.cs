using Carola.DataAccessLayer.Abstract;
using Carola.DataAccessLayer.Concrete;
using Carola.DataAccessLayer.Repository;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carola.DataAccessLayer.EntityFramework
{
    public class EfCarDal : GenericRepository<Car>, ICarDal
    {
        private readonly CarolaContext _carContext;

        public EfCarDal(CarolaContext context) : base(context)
        {
            _carContext = context;
        }

        public async Task<List<Car>> GetAllCarsWithCategory()
        {
            return await _carContext.Cars
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<int> GetCarCountAsync()
        {
            return await _carContext.Cars.CountAsync();
        }

        public async Task<List<Car>> GetCarsByPAgingAsync(int page, int pageSize)
        {
            var cars =await _carContext.Cars.ToListAsync();
            var pagedCars = cars
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(x => new Car
                            {
                                CarId = x.CarId,
                                DailyPrice = x.DailyPrice,
                                FuelType = x.FuelType,
                                IsAvailable = x.IsAvailable,
                                ImageUrl = x.ImageUrl,
                                LuggageCapacity = x.LuggageCapacity,
                                Model = x.Model,
                                ModelYear = x.ModelYear,
                                Mileage = x.Mileage,
                                TransmissionType = x.TransmissionType,
                                SeatCount = x.SeatCount,
                                PlateNumber = x.PlateNumber,
                                Brand = x.Brand,
                                Category = x.Category,
                                CategoryId = x.CategoryId
                            }).ToList();
            return pagedCars;
        }

        public async Task<List<Car>> GetLast6Cars()
        {
            return await _carContext.Cars.OrderByDescending(x => x.CarId).Take(6).ToListAsync();
        }
    }
}
