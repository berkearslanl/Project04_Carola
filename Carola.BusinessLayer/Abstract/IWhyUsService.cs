using Carola.DtoLayer.Dtos.WhyUsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface IWhyUsService
    {
        Task<List<ResultWhyUsDto>> GetAllWhyUsAsync();
        Task<GetWhyUsByIdDto> GetWhyUsById(int id);
        Task CreateWhyUsAsync(CreateWhyUsDto createWhyUsDto);
        Task UpdateWhyUsAsync(UpdateWhyUsDto updateWhyUsDto);
        Task DeleteWhyUsAsync(int id);
    }
}
