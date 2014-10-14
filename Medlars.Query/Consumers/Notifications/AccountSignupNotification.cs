namespace Medlars.Query.Consumers.Notifications
{
    using log4net;

    using Medlars.Command.Account;

    using TastyDomainDriven;

    public class AccountSignupNotification : IConsumes<SignupExecutedEvent>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AccountSignupNotification));

        public void Consume(SignupExecutedEvent e)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Sending email to {0} with temporary password: {1}", e.Email, e.TemporaryPassword);
            }
        }
    }
}
