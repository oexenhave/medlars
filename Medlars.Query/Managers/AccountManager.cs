using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlars.Query.Managers
{
    using Medlars.Core;
    using Medlars.Query.Models;

    public class AccountManager
    {
        private readonly MedlarsDataContext context;

        public AccountManager(MedlarsDataContext context)
        {
            this.context = context;
        }

        public Account Authenticate(string email, string password)
        {
            var account = context.Accounts.FirstOrDefault(a => a.Email == email);
            if (account != null)
            {
                if (Encryption.GeneratePasswordHash(account.PasswordSalt, password) == account.PasswordHash)
                {
                    return account;
                }
            }

            return null;
        }

        public bool IsEmailInUse(string email)
        {
            return context.Accounts.Any(a => a.Email.ToLower() == email.ToLower());
        }
    }
}
