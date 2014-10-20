namespace Medlars.Query.Consumers.Database
{
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;

    using log4net;

    using Medlars.Command.Account;
    using Medlars.Query.Models;

    using TastyDomainDriven;

    public class AccountView : IConsumes<SignUpSucceededEvent>,
        IConsumes<SignInSucceededEvent>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AccountView));

        private readonly MedlarsDataContext context;

        public AccountView(MedlarsDataContext context)
        {
            this.context = context;
        }

        public void Consume(SignUpSucceededEvent e)
        {
            context.Accounts.Add(new Account
                                 {
                                     Email = e.Email,
                                     Secret = e.Secret,
                                     AccountId = Guid.Parse(e.AggregateId.ToString()),
                                     AllowedIps = e.AllowedIps,
                                     PasswordHash = e.PasswordHash,
                                     PasswordSalt = e.PasswordSalt,
                                     LastLogin = new DateTime(1970, 1, 1)
                                 });

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                if (Logger.IsErrorEnabled)
                {
                    foreach (var result in ex.EntityValidationErrors)
                    {
                        foreach (var error in result.ValidationErrors)
                        {
                            Logger.ErrorFormat("Validation error on {0}: {1}", error.PropertyName, error.ErrorMessage);
                        }
                    }
                }
            }
        }

        public void Consume(SignInSucceededEvent e)
        {
            var accountId = Guid.Parse(e.AggregateId.ToString());
            var account = context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account != null)
            {
                account.LastLogin = e.Timestamp;
                context.SaveChanges();
            }
        }
    }
}