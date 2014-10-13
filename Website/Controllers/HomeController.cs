using System;
using System.Web.Mvc;

namespace Medlars.Website.Controllers
{
    using Medlars.Command.Account;

    using TastyDomainDriven;

    public class HomeController : Controller
    {
        private readonly IBus bus;

        public HomeController(IBus bus)
        {
            this.bus = bus;
        }

        public ActionResult Index()
        {
            bus.Dispatch(new SignupCommand { Id = new SignupId(new Guid("1638FF9C-72E5-4D78-B911-F893827C5C97")), Email = "soeren@oexenhave.dk", Timestamp = DateTime.Now });

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
