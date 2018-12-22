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
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        #region Index
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.City).Include(c => c.Department);
            return View(companies.ToList());
        }
        #endregion

        #region Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();

                if (company.LogoFile != null)
                {
                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}-{1}.jpg", company.CompanyId, company.Name);
                    var respose = FilesHelpers.UploadPhoto(company.LogoFile, folder, file);

                    if (respose)
                    {                     
                        var pic = string.Format("{0}/{1}", folder, file);
                        //si respose es true, actualizamos el logo de la compania
                        company.Logo = pic;
                        //actalizamos la dv
                        db.Entry(company).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", company.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name", company.DepartmentId);
            return View(company);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            // si la compania no es null, armamos
            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", company.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name", company.DepartmentId);
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.LogoFile != null)
                {
                    var pic =string.Empty;

                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}-{1}.jpg", company.CompanyId, company.Name);
                    var respose = FilesHelpers.UploadPhoto(company.LogoFile, folder, file);

                    if (respose)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        //si respose es true, actualizamos el logo de la compania
                        company.Logo = pic;

                    }
                }
                //actalizamos la dv
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", company.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name", company.DepartmentId);
            return View(company);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //Borrar - Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Crea el método para recuperar los datos en el controlador.
        //recibe las ciudades de determinado depatmanento (int departmentId)
        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //retorna las ciudades Where conicidan el ingresado por parametro
            var cities = db.Cities.Where(x => x.DepartmentId == departmentId);
            return Json(cities);
        }
        #endregion


        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
