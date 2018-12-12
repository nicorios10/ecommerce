using ecommerce.Models;
using System;
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

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}