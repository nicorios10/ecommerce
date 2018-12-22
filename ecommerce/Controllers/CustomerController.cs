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
    [Authorize(Roles = "User")]
    public class CustomerController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        #region Index
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var customers = db.Customers.Where(c=>c.CompanyId==user.CompanyId).Include(c => c.City).Include(c => c.Department);
            return View(customers.ToList());
        }
        #endregion

        #region Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name");

            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var customer = new Customer { CompanyId = user.CompanyId };

            return View(customer);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                //El customer tiene rol de Customer
                UsersHelper.CreateUserASP(customer.UserName, "Customer");

                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name", customer.DepartmentId);

            return View(customer);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                // TODO: validar cuando el email cambia

                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name", customer.DepartmentId);

            return View(customer);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();

            //hay que borrarlo como usuario, para que no se pueda seguir loguando
            UsersHelper.DeleteUser(customer.UserName);

            return RedirectToAction("Index");
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
