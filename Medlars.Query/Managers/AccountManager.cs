using System.Linq;

namespace Medlars.Query.Managers
{
    using System;

    using Command.Account;
    using Core;
    using Models;

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
            var account = this.context.Accounts.FirstOrDefault(a => a.Email == email);
            if (account != null)
            {
                if (Encryption.GeneratePasswordHash(account.PasswordSalt, password) == account.PasswordHash)
                {
                    this.bus.Dispatch(new SignInCommand { Id = new AccountId(account.AccountId), Timestamp = DateTime.Now, Ip = userHostAddress, Success = true });
                    return account;
                }

                this.bus.Dispatch(new SignInCommand { Id = new AccountId(account.AccountId), Timestamp = DateTime.Now, Ip = userHostAddress, Success = false });
                return null;
            }

            return null;
        }

        public bool IsEmailInUse(string email)
        {
            return this.context.Accounts.Any(a => a.Email.ToLower() == email.ToLower());
        }
    }
}
