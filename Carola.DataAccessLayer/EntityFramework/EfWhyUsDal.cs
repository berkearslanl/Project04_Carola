using Carola.DataAccessLayer.Abstract;
using Carola.DataAccessLayer.Concrete;
using Carola.DataAccessLayer.Repository;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DataAccessLayer.EntityFramework
{
    public class EfWhyUsDal : GenericRepository<WhyUs>, IWhyUsDal
    {
        public EfWhyUsDal(CarolaContext context) : base(context)
        {

        }
    }
}
