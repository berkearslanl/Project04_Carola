using Carola.DtoLayer.Dtos.LocationDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.LocationValidators
{
    public class CreateLocationDtoValidator : AbstractValidator<CreateLocationDto>
    {
        public CreateLocationDtoValidator()
        {
            RuleFor(x => x.LocationName)
                .NotEmpty().WithMessage("Lokasyon adı boş geçilemez.")
                .Length(2, 60).WithMessage("Lokasyon adı 2 ile 60 karakter arasında olmalıdır.");

            RuleFor(x => x.AuthorizedPerson)
                .NotEmpty().WithMessage("Yetkili kişi adı boş geçilemez.")
                .Length(2, 60).WithMessage("Yetkili kişi adı 2 ile 60 karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Yetkili kişi adı sadece harflerden oluşmalıdır.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş geçilemez.")
                .Length(2, 40).WithMessage("Şehir adı 2 ile 40 karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Şehir adı sadece harflerden oluşmalıdır.");

            RuleFor(x => x.Adress)
                .NotEmpty().WithMessage("Adres boş geçilemez.")
                .Length(5, 200).WithMessage("Adres 5 ile 200 karakter arasında olmalıdır.");
        }
    }
}
