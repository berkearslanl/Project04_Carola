using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DtoLayer.Dtos.GalleryDtos
{
    public class UpdateGalleryDto
    {
        public int GalleryId { get; set; }
        public string ImageUrl { get; set; }
    }
}
