﻿using System;
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
    public class UserController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        #region Index
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.City).Include(u => u.Company).Include(u => u.Department);
            return View(users.ToList());
        }
        #endregion

        #region Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name");
            ViewBag.CompanyId = new SelectList(CombosHelpers.GetCompanies(), "CompanyId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                UsersHelper.CreateUserASP(user.UserName, "User");

                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Users";
                    var file = string.Format("{0}-{1}.jpg", user.UserId, user.FullName);
                    var respose = FilesHelpers.UploadPhoto(user.PhotoFile, folder, file);

                    if (respose)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        //si respose es true, actualizamos el logo de la compania
                        user.Photo = pic;
                        //actalizamos la dv
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelpers.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelpers.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelpers.GetDepartments(), "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PhotoFile != null)
                {
                    var pic = string.Empty;

                    var folder = "~/Content/Users";
                    var file = string.Format("{0}-{1}.jpg", user.UserId,user.FullName);
                    var respose = FilesHelpers.UploadPhoto(user.PhotoFile, folder, file);

                    if (respose)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        //si respose es true, actualizamos el logo de la compania
                        user.Photo = pic;

                    }
                }


                var db2 = new ECommerceContext();
                var currentUser = db2.Users.Find(user.UserId);
                if (currentUser.UserName != user.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, user.UserName);
                }
                db2.Dispose();

                //actalizamos la dv
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(CombosHelpers.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelpers.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();

            //hay que borrarlo como usuario
            UsersHelper.DeleteUser(user.UserName);

            return RedirectToAction("Index");
        }

        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            
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
