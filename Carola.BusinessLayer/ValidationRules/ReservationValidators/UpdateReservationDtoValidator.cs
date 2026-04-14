using Carola.DtoLayer.Dtos.ReservationDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.ReservationValidators
{
    public class UpdateReservationDtoValidator : AbstractValidator<UpdateReservationDto>
    {
        public UpdateReservationDtoValidator()
        {
            RuleFor(x => x.ReservationId)
                .GreaterThan(0).WithMessage("Geçersiz rezervasyon ID.");

            RuleFor(x => x.CarId)
                .GreaterThan(0).WithMessage("Geçerli bir araç seçilmelidir.");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Geçerli bir müşteri seçilmelidir.");

            RuleFor(x => x.PickupLocationId)
                .GreaterThan(0).WithMessage("Alış lokasyonu seçilmelidir.");

            RuleFor(x => x.ReturnLocationId)
                .GreaterThan(0).WithMessage("İade lokasyonu seçilmelidir.");

            RuleFor(x => x.PickupDate)
                .NotEmpty().WithMessage("Alış tarihi boş geçilemez.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Alış tarihi bugün veya sonraki bir tarih olmalıdır.");

            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("İade tarihi boş geçilemez.")
                .GreaterThan(x => x.PickupDate).WithMessage("İade tarihi alış tarihinden sonra olmalıdır.");

            RuleFor(x => x.TotalPrice)
                .GreaterThan(0).WithMessage("Toplam fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.ReservationStatus)
                .NotEmpty().WithMessage("Rezervasyon durumu belirtilmelidir.")
                .Must(s => new[] { "Beklemede", "Onaylandı", "İptal", "Tamamlandı" }.Contains(s))
                .WithMessage("Geçersiz rezervasyon durumu. (Beklemede / Onaylandı / İptal / Tamamlandı)");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
