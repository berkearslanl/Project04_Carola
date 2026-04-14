using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.EntityLayer.Entities
{
    public class Category //internal sadece bu projede kullanılabiliyor(EntityLayer)
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }

        public List<Car> Cars { get; set; }
    }
}
