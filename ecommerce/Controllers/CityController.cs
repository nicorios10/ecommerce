using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Classes;
using ecommerce.Models;

namespace ecommerce.Controllers
{
    public class CityController : Controller
    {
        private ECommerceContext dbContext = new ECommerceContext();

        // GET: City
        public ActionResult Index()
        {
            var cities = dbContext.Cities.Include(c => c.Department);
            return View(cities.ToList());
        }

        // GET: City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = dbContext.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: City/Create
        public ActionResult Create()
        {          
            //el viewBag sirve para conectar datos entre  el controlador y la vista
            ViewBag.DepartmentId = new SelectList(
                CombosHelpers.GetDepartments()
                , "DepartmentId"
                , "Name");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                dbContext.Cities.Add(city);

                try
                {
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                    //si aparece"_Index" estamos duplicando un valor
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same value");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

                return RedirectToAction("Index");
            }

            //OrderBy(x=> x.Name) ordeno el dropDownList por nombre
            ViewBag.DepartmentId = new SelectList(
                CombosHelpers.GetDepartments()
                , "DepartmentId"
                , "Name"
                , city.DepartmentId);
            return View(city);
        }

        // GET: City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = dbContext.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments()
                , "DepartmentId"
                , "Name"
                , city.DepartmentId);
            return View(city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(city).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(
                CombosHelpers.GetDepartments()
                , "DepartmentId"
                , "Name"
                , city.DepartmentId);
            return View(city);
        }

        // GET: City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = dbContext.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = dbContext.Cities.Find(id);
            dbContext.Cities.Remove(city);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
