namespace Medlars.Query.Consumers.Database
{
    using log4net;

    using Medlars.Command.Entry;

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
            Logger.Debug(e.Message);
        }
    }
}
