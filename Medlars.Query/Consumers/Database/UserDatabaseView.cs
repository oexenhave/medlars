namespace Medlars.Query.Consumers.Database
{
    using System;
    using System.Data.Entity.Validation;

    using log4net;

    using Medlars.Command.Account;
    using Medlars.Query.Models;

    using TastyDomainDriven;

    public class UserDatabaseView : IConsumes<SignupExecutedEvent>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserDatabaseView));

        private readonly MedlarsDataContext context;

        public UserDatabaseView(MedlarsDataContext context)
        {
            this.context = context;
        }

        public void Consume(SignupExecutedEvent e)
        {
            context.Users.Add(new User { Email = e.Email, Secret = e.Secret, UserId = Guid.Parse(e.AggregateId.ToString()) });

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