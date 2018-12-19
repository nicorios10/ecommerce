using ecommerce.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerce.Classes
{
    //
    public class CombosHelpers : IDisposable
    {
        private static ECommerceContext dbContext = new ECommerceContext();


        public static List<Department> GetDepartments()
        {
            //to list es el equivalente a select * from departments
            var departments = dbContext.Departments.ToList();

            //agrego un objeto en memoria
            departments.Add(new Department
            {
                DepartmentId = 0,
                Name = "[Seleccióne...]"//[] para que lo ponga primero y no ordene x name
            });

            return departments.OrderBy(x => x.Name).ToList();
        }

        public static List<City> GetCities()
        {
            //to list es el equivalente a select * from departments
            var cities = dbContext.Cities.ToList();

            //agrego un objeto en memoria
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Seleccióne...]"//[] para que lo ponga primero y no ordene x name
            });

            return cities.OrderBy(x => x.Name).ToList();
        }

        public static List<Company> GetCompanies()
        {
            var company = dbContext.Companies.ToList();

            company.Add(new Company
            {
                CityId = 0,
                Name = "[Seleccióne...]"
            });

            return company.OrderBy(x => x.Name).ToList();
        }

        public static List<Category> GetCategories(int companyId)
        {
            var categories = dbContext.Categories.Where(c=>c.CompanyId==companyId).ToList();

            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[Seleccióne...]"
            });

            return categories.OrderBy(x => x.Description).ToList();

        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes = dbContext.Taxes.Where(c => c.CompanyId == companyId).ToList();

            taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[Seleccióne...]"
            });

            return taxes.OrderBy(x => x.Description).ToList();

        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}