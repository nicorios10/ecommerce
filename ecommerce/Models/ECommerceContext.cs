using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ecommerce.Models
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext() : base("DefaultConnection")
        {

        }

        //metodo para que desabilitar el borrado en cascada cuando es uno a muchos
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<City> Cities { get; set; }

        public System.Data.Entity.DbSet<ecommerce.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<ecommerce.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<ecommerce.Models.Category> Categories { get; set; }
    }
}