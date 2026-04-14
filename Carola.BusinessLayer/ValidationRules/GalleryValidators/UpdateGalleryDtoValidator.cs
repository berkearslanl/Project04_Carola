using Carola.DtoLayer.Dtos.GalleryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.ValidationRules.GalleryValidators
{
    public class UpdateGalleryDtoValidator:AbstractValidator<UpdateGalleryDto>
    {
        public UpdateGalleryDtoValidator()
        {
            RuleFor(x => x.GalleryId)
                .GreaterThan(0).WithMessage("Geçerli bir Galeri ID girilmelidir.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL'si boş geçilemez.")
                .Length(5, 500).WithMessage("Görsel URL'si 5 ile 500 karakter arasında olmalıdır.");
        }
    }
}
