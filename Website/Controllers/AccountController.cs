namespace Medlars.Website.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;

    using Medlars.Command.Account;
    using Medlars.Core;
    using Medlars.Query.Managers;

    using Microsoft.AspNet.Identity;

    using TastyDomainDriven;

    public class AccountController : Controller
    {
        private readonly IBus bus;

        private readonly AccountManager accountManager;

        public AccountController(IBus bus, AccountManager accountManager)
        {
            this.bus = bus;
            this.accountManager = accountManager;
        }

        public ActionResult Index()
        {
            return this.RedirectToAction("Authenticate");
        }

        public ActionResult Authenticate()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Authenticate(string email, string password)
        {
            this.ViewBag.Email = email;

            var account = this.accountManager.Authenticate(email, password, this.Request.UserHostAddress);
            if (account != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
                var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = this.Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(id);

                return this.View("Authenticated");
            }

            this.ModelState.AddModelError("email", "Unable to verify email and password combination. Please try again.");
            return this.View();
        }

        public ActionResult Signup()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Signup(string email, bool? terms)
        {
            this.ViewBag.Email = email;

            if (email.IsNullOrInvalidEmail())
            {
                this.ModelState.AddModelError("email", "Email invalid");
                return this.View();
            }

            if (this.accountManager.IsEmailInUse(email))
            {
                this.ModelState.AddModelError("email", "Email already in use");
                return this.View();
            }

            this.bus.Dispatch(new SignUpCommand { Id = new AccountId(Guid.NewGuid()), Email = email, Timestamp = DateTime.Now });
            return this.View("SignedUp");
        }

        public ActionResult SignOut()
        {
            var ctx = this.Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return this.View("SignedOut");
        }
    }
}
