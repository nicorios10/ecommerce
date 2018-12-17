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
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private ECommerceContext dbContext = new ECommerceContext();

        #region Index      
        public ActionResult Index()
        {
            return View(dbContext.Departments.ToList());
        }
        #endregion

        #region Details       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = dbContext.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create POST       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                dbContext.Departments.Add(department);
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
            }
            return View(department);
        }
        #endregion

        #region Edit       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = dbContext.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        #endregion

        #region Edit POST       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(department).State = EntityState.Modified;
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
            }
            return View(department);
        }
        #endregion

        #region Delete     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = dbContext.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        #endregion

        #region Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = dbContext.Departments.Find(id);
            dbContext.Departments.Remove(department);
            try
            {
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                //si aparece"REFERENCE"estamos violando una regla de integridad referencial
                {
                    ModelState.AddModelError(string.Empty, "The record can't be delete because it have related records");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            };
            //si hubo error lo voy a devolver a la vista
            return View(department);
        }
        #endregion


        #region *Dispose*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
