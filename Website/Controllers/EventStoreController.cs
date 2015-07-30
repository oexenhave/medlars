using System.Web.Mvc;

namespace Medlars.Website.Controllers
{
    using Autofac;

    using Medlars.Command;
    using Medlars.Query;

    using TastyDomainDriven;

    public class EventStoreController : Controller
    {
        private const string AllowedIps = "::1 127.0.0.1";

        private readonly IEventStore eventStorage;

        private readonly MedlarsDataContext context;

        private readonly ILifetimeScope scope;

        public EventStoreController(IEventStore eventStorage, MedlarsDataContext context, ILifetimeScope scope)
        {
            this.eventStorage = eventStorage;
            this.context = context;
            this.scope = scope;
        }

        public ActionResult Index()
        {
            if (Request.UserHostAddress != null && !AllowedIps.Contains(Request.UserHostAddress))
            {
                return this.Redirect("~/");
            }

            return View();
        }

        public ActionResult ResetDatabase()
        {
            context.ResetDatabase();
            return this.RedirectToAction("Index");
        }

        public ActionResult ReplayEvents()
        {
            //var projector = scope.Resolve<MedlarsProjectionFactory>();
            //var events = scope.Resolve<IEventStore>().ReplayAll().Events;
            //foreach (var e in events)
            //{
            //    projector.ConsumeByReadSide((object)e);
            //}

            return this.RedirectToAction("Index");
        }

        public ActionResult TruncateEventStorage()
        {
            context.TruncateEventStorage();
            return this.RedirectToAction("Index");
        }
    }
}