namespace Medlars.Query.Consumers.Database
{
    using System;

    using log4net;

    using Medlars.Command.Entry;
    using Medlars.Query.Models;

    using TastyDomainDriven;

    public class EntryView : IConsumes<StringAddedEvent>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EntryView));

        private readonly MedlarsDataContext context;

        public EntryView(MedlarsDataContext context)
        {
            this.context = context;
        }

        public void Consume(StringAddedEvent e)
        {
            int severity;

            switch (e.Severity)
            {
                case EntrySeverity.Debug:
                    severity = 0;
                    break;
                case EntrySeverity.Info:
                    severity = 10;
                    break;
                case EntrySeverity.Warning:
                    severity = 20;
                    break;
                case EntrySeverity.Error:
                    severity = 30;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            context.Entries.Add(new Entry
                                {
                                    AccountId = Guid.Parse(e.AccountId.ToString()),
                                    EntryId = Guid.Parse(e.AggregateId.ToString()),
                                    Message = e.Message,
                                    Service = e.Service,
                                    Severity = severity,
                                    Timestamp = e.Timestamp
                                });

            context.SaveChanges();
        }
    }
}
