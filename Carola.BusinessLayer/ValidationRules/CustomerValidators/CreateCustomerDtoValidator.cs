using Carola.DtoLayer.Dtos.CustomerDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.CustomerValidators
{
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş geçilemez.")
                .Length(2, 40).WithMessage("Ad 2 ile 40 karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Ad sadece harflerden oluşmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş geçilemez.")
                .Length(2, 40).WithMessage("Soyad 2 ile 40 karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Soyad sadece harflerden oluşmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş geçilemez.")
                .Matches(@"^(\+90|0)?[5][0-9]{9}$")
                .WithMessage("Geçerli bir Türkiye telefon numarası giriniz. (Örn: 05XX XXX XX XX)");

            RuleFor(x => x.DriverLicenseNumber)
                .NotEmpty().WithMessage("Ehliyet numarası boş geçilemez.")
                .Length(5, 20).WithMessage("Ehliyet numarası 5 ile 20 karakter arasında olmalıdır.")
                .Matches(@"^[A-Z0-9]+$").WithMessage("Ehliyet numarası yalnızca büyük harf ve rakamlardan oluşmalıdır.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doğum tarihi boş geçilemez.")
                .LessThan(DateTime.Today.AddYears(-18)).WithMessage("Müşteri en az 18 yaşında olmalıdır.")
                .GreaterThan(new DateTime(1900, 1, 1)).WithMessage("Geçerli bir doğum tarihi giriniz.");
        }
    }
}
