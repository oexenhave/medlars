using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlars.Tests
{
    using Autofac;

    using Medlars.Command.Account;
    using Medlars.Website;

    using TastyDomainDriven;

    using Xunit;

    public class AccountTests : BaseTest
    {
        private readonly AccountId accountId;

        public AccountTests()
        {
            this.accountId = new AccountId(new Guid("31C69AFF-BCFD-47AB-820E-799341F7F822"));
        }

        [Fact]
        public void SignupSuccessfully()
        {
            this.Given(
                this.SetupContainer(),
                new List<ICommand> { this.SignUp(), },
                (eventStore) =>
                {
                    var stream = eventStore.ReplayAll();
                    Assert.Equal(1, stream.Events.Count);
                });
        }

        [Fact]
        public void SignupWithInvalidEmail()
        {
            this.Given(this.SetupContainer(), new List<ICommand>(), (bus) => Assert.Throws<EmailInvalidException>(() => bus.Dispatch(this.SignUp(string.Empty))));
            this.Given(this.SetupContainer(), new List<ICommand>(), (bus) => Assert.Throws<EmailInvalidException>(() => bus.Dispatch(this.SignUp("invalid@emailaddress"))));
        }

        [Fact]
        public void SignupTwiceWithSameEmail()
        {
            this.Given(this.SetupContainer(), new List<ICommand> { this.SignUp() }, (container) => Assert.Equal(1, container.ReplayAll().Events.Count));
            this.Given(this.SetupContainer(), new List<ICommand>(), (bus) => Assert.Throws<AccountExistsException>(() => bus.Dispatch(this.SignUp())));
        }

        [Fact]
        public void SignInSuccessfully()
        {
            this.Given(this.SetupContainer(), new List<ICommand> { this.SignUp(), this.SignIn() }, (container) => Assert.Equal(2, container.ReplayAll().Events.Count));
        }

        [Fact]
        public void SignInWithoutSignup()
        {
            this.Given(this.SetupContainer(), new List<ICommand>(), (bus) => Assert.Throws<AccountNotFoundException>(() => bus.Dispatch(this.SignIn())));
        }

        private ICommand SignUp(string email = "soeren@oexenhave.dk")
        {
            return new SignUpCommand { Email = email, Id = accountId, Timestamp = DateTime.Now };
        }

        private ICommand SignIn(string ip = "127.0.0.1", bool success = true)
        {
            return new SignInCommand { Id = accountId, Timestamp = DateTime.Now, Ip = ip, Success = success };
        }
    }
}