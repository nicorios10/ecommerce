using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;

namespace ecommerce.Controllers
{
    [Authorize(Roles ="User")]
    public class CategoryController : Controller
    {
        private ECommerceContext db = new ECommerceContext();
        
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            //validamos que exista el usuario
            if (user == null)
            {
                return RedirectToAction("Index","Home");
            }

            //Le decimos que solo nos busque las categorias del usuario logueado
            var categories = db.Categories.Where(c=>c.CompanyId==user.CompanyId);

            return View(categories.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            //validamos que exista
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var category = new Category
            {
                CompanyId = user.CompanyId
            };

            return View(category);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Description,CompanyId")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", category.CompanyId);
            return View(category);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", category.CompanyId);
            return View(category);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
