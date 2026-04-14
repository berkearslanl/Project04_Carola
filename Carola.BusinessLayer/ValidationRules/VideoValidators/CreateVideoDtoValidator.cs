using Carola.DtoLayer.Dtos.VideoDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.VideoValidators
{
    public class CreateVideoDtoValidator : AbstractValidator<CreateVideoDto>
    {
        public CreateVideoDtoValidator()
        {
            RuleFor(x => x.VideoUrl)
                .NotEmpty().WithMessage("Video URL'si boş geçilemez.")
                .Length(5, 1000).WithMessage("Video URL'si 5 ile 1000 karakter arasında olmalıdır.");
        }
    }
}
