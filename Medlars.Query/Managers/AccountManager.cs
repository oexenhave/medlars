using System.Linq;

namespace Medlars.Query.Managers
{
    using System;
    using System.Security;

    using Medlars.Command.Account;
    using Medlars.Core;
    using Medlars.Query.Models;

    using TastyDomainDriven;

    public class AccountManager
    {
        private readonly MedlarsDataContext context;

        private readonly IBus bus;

        public AccountManager(MedlarsDataContext context, IBus bus)
        {
            this.context = context;
            this.bus = bus;
        }

        public Account Authenticate(string email, string password, string userHostAddress)
        {
            var account = context.Accounts.FirstOrDefault(a => a.Email == email);
            if (account != null)
            {
                if (Encryption.GeneratePasswordHash(account.PasswordSalt, password) == account.PasswordHash)
                {
                    bus.Dispatch(new SignInCommand { Id = new AccountId(account.AccountId), Timestamp = DateTime.Now, Ip = userHostAddress, Success = true });
                    return account;
                }

                bus.Dispatch(new SignInCommand { Id = new AccountId(account.AccountId), Timestamp = DateTime.Now, Ip = userHostAddress, Success = false });
                return null;
            }

            return null;
        }

        public bool IsEmailInUse(string email)
        {
            return context.Accounts.Any(a => a.Email.ToLower() == email.ToLower());
        }
    }
}
