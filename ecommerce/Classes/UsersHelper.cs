﻿using ecommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ecommerce.Classes
{
    public class UsersHelper : IDisposable //por que abro 2 contextos
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static ECommerceContext dbContext = new ECommerceContext();

        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            //chekea a ver si el rol existe, sino, lo crea
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        //garatiza que exista el superusuario
        public static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassword"];
            var userASP = userManager.FindByName(email);

            if (userASP == null)
            {
                CreateUserASP(email, "Admin", password);
                return;
            }
            userManager.AddToRole(userASP.Id, "Admin");
        }

        public static void CreateUserASP(string email, string roleName)
        {
            var userManager=new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email
            };

            userManager.Create(userASP, email);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CreateUserASP(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static async Task PasswordRecovery(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);

            if (userASP == null)
            {
                return;
            }

            var user = dbContext.Users.Where(x => x.UserName == email).FirstOrDefault();
            if (user == null)
            {
                return;
            }

            var random = new Random();
            var newPassword= string.Format("{0}{1}{2:04}*",
                user.FirstName.Trim().ToUpper().Substring(0,1),
                user.LastName.Trim().ToLower(),
                random.Next(10000));

            userManager.RemovePassword(userASP.Id);
            userManager.AddPassword(userASP.Id, newPassword);

            var subject = "Taxes Password Recovery";
            var body = string.Format(@"
                    <h1>Taxes Password RECOVERY</h1>
                    <p>You new pass is: <em>{0}</em></p>
                    <p>Please change it for onother</p>", newPassword);

            await MailHelper.SendMail(email, subject, body);
        }


        public void Dispose()
        {
            userContext.Dispose();
            dbContext.Dispose();
        }
    }
}