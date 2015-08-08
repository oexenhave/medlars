using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Medlars.Query.Consumers.Database;
using TastyDomainDriven.Projections;

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

        public ActionResult List()
        {
            var query = eventStorage.ReplayAll().Events.AsQueryable();

            query = query.Reverse();
            query = query.Take(50);

            var data = query.Select(e => new { EventType = e.GetType().ToString(), Id = e.AggregateId.ToString(), Date = e.Timestamp.ToString("o"), Event = e }).ToArray();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReplayEvents()
        {
            var _readRegister = new EventRegister(typeof(IConsumes<>));
            _readRegister.Subscribe(new AccountView(context));
            foreach (var item in eventStorage.ReplayAll().Events)
            {
                _readRegister.Consume(item);
            }

            return this.RedirectToAction("Index");
        }

        public ActionResult TruncateEventStorage()
        {
            context.TruncateEventStorage();
            return this.RedirectToAction("Index");
        }
    }
}