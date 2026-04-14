using Carola.DtoLayer.Dtos.WhyUsDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.ValidationRules.WhyUsValidators
{
    public class CreateWhyUsDtoValidator:AbstractValidator<CreateWhyUsDto>
    {
        public CreateWhyUsDtoValidator()
        {
            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("İkon URL'si boş geçilemez.")
                .MaximumLength(200).WithMessage("İkon URL'si en fazla 200 karakter olabilir.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş geçilemez.")
                .Length(2, 100).WithMessage("Başlık 2 ile 100 karakter arasında olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez.")
                .Length(10, 500).WithMessage("Açıklama 10 ile 500 karakter arasında olmalıdır.");
        }
    }
}
