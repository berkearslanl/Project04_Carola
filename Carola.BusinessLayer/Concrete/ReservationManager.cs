using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.ReservationDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class ReservationManager : IReservationService
    {
        private readonly IReservationDal _reservationDal;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReservationDto> _createValidator;
        private readonly IValidator<UpdateReservationDto> _updateValidator;

        public ReservationManager(IReservationDal reservationDal, IMapper mapper, IValidator<UpdateReservationDto> updateValidator, IValidator<CreateReservationDto> createValidator)
        {
            _reservationDal = reservationDal;
            _mapper = mapper;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        public async Task CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            var result = _createValidator.Validate(createReservationDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var values = _mapper.Map<Reservation>(createReservationDto);
            await _reservationDal.InsertAsync(values);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationDal.DeleteAsync(id);
        }

        public async Task<List<ResultReservationDto>> GetAllReservationAsync()
        {
            var values = await _reservationDal.GetAllAsync();
            return _mapper.Map<List<ResultReservationDto>>(values);
        }

        public async Task<GetReservationByIdDto> GetReservationById(int id)
        {
            var values = await _reservationDal.GetByIdAsync(id);
            return _mapper.Map<GetReservationByIdDto>(values);
        }

        public async Task UpdateReservationAsync(UpdateReservationDto updateReservationDto)
        {

            var result = _updateValidator.Validate(updateReservationDto);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            var values = _mapper.Map<Reservation>(updateReservationDto);
            await _reservationDal.UpdateAsync(values);
        }

        public async Task UpdateReservationStatusAsync(int reservationId, string status)
        {
            var reservation = await _reservationDal.GetByIdAsync(reservationId);
            reservation.ReservationStatus = status;
            await _reservationDal.UpdateAsync(reservation);
        }
    }
}
