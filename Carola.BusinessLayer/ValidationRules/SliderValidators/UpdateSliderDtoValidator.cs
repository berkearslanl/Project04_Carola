using Carola.DtoLayer.Dtos.SliderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.ValidationRules.SliderValidators
{
    public class UpdateSliderDtoValidator:AbstractValidator<UpdateSliderDto>
    {
        public UpdateSliderDtoValidator()
        {
            RuleFor(x => x.SliderId)
                .GreaterThan(0).WithMessage("Geçerli bir Slider ID girilmelidir.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş geçilemez.")
                .Length(2, 100).WithMessage("Başlık 2 ile 100 karakter arasında olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez.")
                .Length(10, 500).WithMessage("Açıklama 10 ile 500 karakter arasında olmalıdır.");

            RuleFor(x => x.BackgroundImage)
                .NotEmpty().WithMessage("Arka plan görseli URL'si boş geçilemez.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Araç görseli URL'si boş geçilemez.");
        }
    }
}
