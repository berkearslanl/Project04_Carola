using Carola.DtoLayer.Dtos.VideoDtos;
using FluentValidation;

namespace Carola.BusinessLayer.ValidationRules.VideoValidators
{
    public class UpdateVideoDtoValidator : AbstractValidator<UpdateVideoDto>
    {
        public UpdateVideoDtoValidator()
        {
            RuleFor(x => x.VideoId)
                .GreaterThan(0).WithMessage("Geçerli bir Video ID girilmelidir.");

            RuleFor(x => x.VideoUrl)
                .NotEmpty().WithMessage("Video URL'si boş geçilemez.")
                .Length(5, 1000).WithMessage("Video URL'si 5 ile 1000 karakter arasında olmalıdır.");
        }
    }
}
