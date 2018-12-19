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
    [Authorize(Roles ="User")]
    public class ProductController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        public ActionResult Index()
        {
            var user = db.Users.Where(u=>u.UserName==User.Identity.Name).FirstOrDefault();

            //include son los inerjoin
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.Tax)
                .Where(p=>p.CompanyId==user.CompanyId);

            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.CategoryId = new SelectList(CombosHelpers.GetCategories(user.CompanyId), "CategoryId", "Description");
            ViewBag.TaxId = new SelectList(CombosHelpers.GetTaxes(user.CompanyId), "TaxId", "Description");

            var product = new Product { CompanyId = user.CompanyId };

            return View(product);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                if (product.ImageFile != null)
                {
                    var folder = "~/Content/Products";
                    var file = string.Format("{0}-{1}.jpg", product.ProductId, product.Description);
                    var respose = FilesHelpers.UploadPhoto(product.ImageFile, folder, file);

                    if (respose)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        //si respose es true, actualizamos el logo de la compania
                        product.Image = pic;
                        //actalizamos la dv
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CombosHelpers.GetCategories(user.CompanyId), "CategoryId", "Description", product.CategoryId);
            ViewBag.TaxId = new SelectList(CombosHelpers.GetTaxes(user.CompanyId), "TaxId", "Description", product.TaxId);
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(CombosHelpers.GetCategories(product.CompanyId), "CategoryId", "Description", product.CategoryId);
            ViewBag.TaxId = new SelectList(CombosHelpers.GetTaxes(product.CompanyId), "TaxId", "Description", product.TaxId);

            return View(product);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    var pic = string.Empty;

                    var folder = "~/Content/Products";
                    var file = string.Format("{0}-{1}.jpg", product.ProductId, product.Description);
                    var respose = FilesHelpers.UploadPhoto(product.ImageFile, folder, file);

                    if (respose)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        product.Image = pic;
                    }
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CombosHelpers.GetCategories(product.CompanyId), "CategoryId", "Description", product.CategoryId);
            ViewBag.TaxId = new SelectList(CombosHelpers.GetTaxes(product.CompanyId), "TaxId", "Description", product.TaxId);

            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
