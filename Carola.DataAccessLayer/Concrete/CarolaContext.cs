using Carola.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DataAccessLayer.Concrete
{
    public class CarolaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-5Q1ARH5E;Database=Project04CarolaDb;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<WhyUs> WhyUs { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Video> Videos { get; set; }

    }
}
