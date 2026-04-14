using Carola.DtoLayer.Dtos.CategoryDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.CategoryValidators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçersiz kategori ID.");

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                .Length(2, 30).WithMessage("Kategori adı 2 ile 30 karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Kategori adı sadece harflerden oluşmalıdır.");
        }
    }
}
