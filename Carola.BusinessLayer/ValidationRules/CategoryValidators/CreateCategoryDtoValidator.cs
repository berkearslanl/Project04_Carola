using Carola.DtoLayer.Dtos.CategoryDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.CategoryValidators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                .Length(2, 30).WithMessage("Kategori adı 2 ile 30 karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Kategori adı sadece harflerden oluşmalıdır.");
        }
    }
}
