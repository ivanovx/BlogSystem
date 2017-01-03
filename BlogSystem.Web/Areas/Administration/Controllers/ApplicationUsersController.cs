namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Identity;
    using Data.Models;
    using Data.UnitOfWork;
    using Infrastructure.Extensions;
    using Common;
    using InputModels.ApplicationUsers;
    using ViewModels.ApplicationUsers;
    using Base;

    public class ApplicationUsersController : AdministrationController
    {
        private readonly IBlogSystemData data;

        public ApplicationUsersController(IBlogSystemData data)
        {
            this.data = data;
        }

        // GET: Administration/ApplicationUsers
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.data.Users.All().Count() / (decimal) perPage);

            var users = this.data.Users
                .All()
                .OrderByDescending(u => u.CreatedOn)
                .To<ApplicationUserViewModel>()
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .ToList();

            var model = new IndexPageViewModel
            {
                Users = users,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        // GET: Administration/ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.data.Users.Find(id);

            if (user == null)
            {
                return this.HttpNotFound();
            }

            return this.View(user);
        }

        // GET: Administration/ApplicationUsers/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = this.data.Users.Find(id);

            if (applicationUser == null)
            {
                return this.HttpNotFound();
            }

            ApplicationUserEditModel model = new ApplicationUserEditModel
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email, 
                UserName = applicationUser.UserName, 
                PasswordHash = applicationUser.PasswordHash, 
                SecurityStamp = applicationUser.SecurityStamp, 
                CreatedOn = applicationUser.CreatedOn
            };

            return this.View(model);
        }

        // POST: Administration/ApplicationUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUserEditModel applicationUser)
        {
            if (this.ModelState.IsValid)
            {
                var user = this.data.Users.Find(applicationUser.Id);

                var checkEmail = this.data.Users
                    .All()
                    .Any(e => e.Email == applicationUser.Email);

                var checkUsername = this.data.Users
                    .All()
                    .Any(u => u.UserName == applicationUser.UserName);

                if (checkEmail && user.Email != applicationUser.Email)
                {
                    return this.Content($"Email {applicationUser.Email} is already taken.");
                }

                if (checkUsername && user.UserName != applicationUser.UserName)
                {
                    return this.Content($"Username {applicationUser.UserName} is already taken.");
                }

                user.Id = applicationUser.Id;
                user.Email = applicationUser.Email;
                user.UserName = applicationUser.UserName;
                user.CreatedOn = applicationUser.CreatedOn;

                if (user.PasswordHash != applicationUser.PasswordHash)
                {
                    var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                    var newHashedPassword = userManager.PasswordHasher.HashPassword(applicationUser.PasswordHash);

                    user.PasswordHash = newHashedPassword;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                }

                this.data.Users.Update(user);
                this.data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(applicationUser);
        }

        // GET: Administration/ApplicationUsers/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/ApplicationUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUserCreateModel userModel)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    Email = userModel.Email, 
                    UserName = userModel.UserName, 
                    PasswordHash = userModel.Password, 
                    CreatedOn = DateTime.Now
                };

                var userManager = this.HttpContext
                    .GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();

                var userCreateResult = userManager.Create(applicationUser, applicationUser.PasswordHash);

                if (!userCreateResult.Succeeded)
                {
                    return this.Content(string.Join("; ", userCreateResult.Errors));
                }

                this.data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(userModel);
        }
    }
}