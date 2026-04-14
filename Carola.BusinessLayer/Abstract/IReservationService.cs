using Carola.DtoLayer.Dtos.ReservationDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface IReservationService 
    {
        Task<List<ResultReservationDto>> GetAllReservationAsync();
        Task<GetReservationByIdDto> GetReservationById(int id);
        Task CreateReservationAsync(CreateReservationDto createReservationDto);
        Task UpdateReservationAsync(UpdateReservationDto updateReservationDto);
        Task DeleteReservationAsync(int id);

        Task UpdateReservationStatusAsync(int reservationId, string status);
    }
}
