using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DtoLayer.Dtos.SliderDtos
{
    public class GetSliderByIdDto
    {
        public int SliderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BackgroundImage { get; set; }
        public string Image { get; set; }
    }
}
