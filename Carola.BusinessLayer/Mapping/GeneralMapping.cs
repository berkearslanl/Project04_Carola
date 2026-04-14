using AutoMapper;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.DtoLayer.Dtos.CustomerDtos;
using Carola.DtoLayer.Dtos.GalleryDtos;
using Carola.DtoLayer.Dtos.VideoDtos;
using Carola.DtoLayer.Dtos.LocationDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;
using Carola.DtoLayer.Dtos.SliderDtos;
using Carola.DtoLayer.Dtos.WhyUsDtos;
using Carola.EntityLayer.Entities;

namespace Carola.BusinessLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {

            // Customer
            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetCustomerByIdDto>().ReverseMap();

            // Brand
            CreateMap<Brand, ResultBrandDto>().ReverseMap();
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, GetBrandByIdDto>().ReverseMap();

            // Category
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            // Location
            CreateMap<Location, ResultLocationDto>().ReverseMap();
            CreateMap<Location, CreateLocationDto>().ReverseMap();
            CreateMap<Location, UpdateLocationDto>().ReverseMap();
            CreateMap<Location, GetLocationByIdDto>().ReverseMap();

            // Car
            CreateMap<Car, ResultCarDto>()
                .ForMember(dest => dest.CategoryName,
                           opt  => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : ""))
                .ReverseMap();
            CreateMap<Car, CreateCarDto>().ReverseMap();
            CreateMap<Car, UpdateCarDto>().ReverseMap();
            CreateMap<Car, GetCarByIdDto>().ReverseMap();

            // Reservation
            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();

            //Slider
            CreateMap<Slider, ResultSliderDto>().ReverseMap();
            CreateMap<Slider, CreateSliderDto>().ReverseMap();
            CreateMap<Slider, UpdateSliderDto>().ReverseMap();
            CreateMap<Slider, GetSliderByIdDto>().ReverseMap();

            //WhyUs
            CreateMap<WhyUs, ResultWhyUsDto>().ReverseMap();
            CreateMap<WhyUs, CreateWhyUsDto>().ReverseMap();
            CreateMap<WhyUs, UpdateWhyUsDto>().ReverseMap();
            CreateMap<WhyUs, GetWhyUsByIdDto>().ReverseMap();

            //Gallery
            CreateMap<Gallery, ResultGalleryDto>().ReverseMap();
            CreateMap<Gallery, CreateGalleryDto>().ReverseMap();
            CreateMap<Gallery, UpdateGalleryDto>().ReverseMap();
            CreateMap<Gallery, GetGalleryByIdDto>().ReverseMap();

            //Video
            CreateMap<Video, ResultVideoDto>().ReverseMap();
            CreateMap<Video, CreateVideoDto>().ReverseMap();
            CreateMap<Video, UpdateVideoDto>().ReverseMap();
            CreateMap<Video, GetVideoByIdDto>().ReverseMap();
        }
    }
}
