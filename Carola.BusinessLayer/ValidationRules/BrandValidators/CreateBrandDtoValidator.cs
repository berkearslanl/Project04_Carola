using Carola.DtoLayer.Dtos.BrandDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.BrandValidators
{
    public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("Marka adı boş geçilemez.")
                .Length(2, 40).WithMessage("Marka adı 2 ile 40 karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Marka adı sadece harflerden oluşmalıdır.")
                .NotEqual("Test").WithMessage("Geçersiz marka adı.")
                .Must(name => !name.Contains("@") && !name.Contains("#")).WithMessage("Marka adı özel karakter içeremez.");

            RuleFor(x => x.LogoUrl)
                .NotEmpty().WithMessage("Logo adresi boş geçilemez.");

            RuleFor(x => x.Status)
                .NotNull().WithMessage("Durum bilgisi belirtilmelidir.");

            When(x => x.Status == true, () =>
            {
                RuleFor(x => x.LogoUrl)
                    .NotEmpty().WithMessage("Aktif markalar için logo zorunludur.");
            });
        }
    }
}
