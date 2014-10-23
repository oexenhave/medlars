namespace Medlars.Tests.Commands
{
    using System;
    using System.Collections.Generic;

    using Autofac;

    using Medlars.Command.Account;

    using TastyDomainDriven;

    using Xunit;

    public class AccountTests : BaseCommandTest
    {
        public static readonly AccountId AccountId = new AccountId(new Guid("31C69AFF-BCFD-47AB-820E-799341F7F822"));

        public static ICommand SignUp(string email = "soeren@oexenhave.dk")
        {
            return new SignUpCommand { Email = email, Id = AccountId, Timestamp = DateTime.Now };
        }

        public static ICommand SignIn(string ip = "127.0.0.1", bool success = true)
        {
            return new SignInCommand { Id = AccountId, Timestamp = DateTime.Now, Ip = ip, Success = success };
        }

        [Fact]
        public void SignupSuccessfully()
        {
            this.Given(
                this.SetupContainer(),
                new List<ICommand> { SignUp(), },
                eventStore =>
                {
                    var stream = eventStore.ReplayAll();
                    Assert.Equal(1, stream.Events.Count);
                });
        }

        [Fact]
        public void SignupWithInvalidEmail()
        {
            this.Given(this.SetupContainer(), new List<ICommand>(), bus => Assert.Throws<EmailInvalidException>(() => bus.Dispatch(SignUp(string.Empty))));
            this.Given(this.SetupContainer(), new List<ICommand>(), bus => Assert.Throws<EmailInvalidException>(() => bus.Dispatch(SignUp("invalid@emailaddress"))));
        }

        [Fact]
        public void SignupTwiceWithSameEmail()
        {
            this.Given(this.SetupContainer(), new List<ICommand> { SignUp() }, bus => Assert.Throws<AccountExistsException>(() => bus.Dispatch(SignUp())));
        }

        [Fact]
        public void SignInSuccessfully()
        {
            this.Given(this.SetupContainer(), new List<ICommand> { SignUp(), SignIn() }, container => Assert.Equal(2, container.ReplayAll().Events.Count));
        }

        [Fact]
        public void SignInWithoutSignup()
        {
            this.Given(this.SetupContainer(), new List<ICommand>(), bus => Assert.Throws<AccountNotFoundException>(() => bus.Dispatch(SignIn())));
        }

        public override void RegisterTestTypes(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().AsImplementedInterfaces();
        }
    }
}