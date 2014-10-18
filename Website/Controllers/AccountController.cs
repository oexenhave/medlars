namespace Medlars.Website.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;

    using Medlars.Command.Account;
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

        public ActionResult Authenticate()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Authenticate(string email, string password)
        {
            var account = accountManager.Authenticate(email, password);
            if (account != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
                var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(id);

                bus.Dispatch(new SignInCommand { Id = new AccountId(account.AccountId), Timestamp = DateTime.Now, Ip = Request.UserHostAddress });
                return this.View("Authenticated");
            }

            return this.View();
        }

        public ActionResult Signup()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Signup(string email)
        {
            return this.View();
        }
    }
}
