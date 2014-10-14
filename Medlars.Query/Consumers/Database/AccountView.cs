namespace Medlars.Query.Consumers.Database
{
    using System;
    using System.Data.Entity.Validation;

    using log4net;

    using Medlars.Command.Account;
    using Medlars.Query.Models;

    using TastyDomainDriven;

    public class AccountView : IConsumes<SignupExecutedEvent>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AccountView));

        private readonly MedlarsDataContext context;

        public AccountView(MedlarsDataContext context)
        {
            this.context = context;
        }

        public void Consume(SignupExecutedEvent e)
        {
            context.Accounts.Add(new Account
                                 {
                                     Email = e.Email,
                                     Secret = e.Secret,
                                     AccountId = Guid.Parse(e.AggregateId.ToString()),
                                     AllowedIps = e.AllowedIps,
                                     PasswordHash = e.PasswordHash,
                                     PasswordSalt = e.PasswordSalt
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
    }
}