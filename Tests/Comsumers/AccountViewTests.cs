namespace Medlars.Tests.Comsumers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;

    using Medlars.Command.Account;
    using Medlars.Query.Consumers.Database;
    using Medlars.Tests.Commands;

    using TastyDomainDriven;

    using Xunit;

    public class AccountViewTests : BaseQueryTest
    {
        public override void RegisterTestTypes(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().AsImplementedInterfaces();
            builder.RegisterType<AccountView>().AsImplementedInterfaces();
        }

        [Fact]
        public void CreateAccountForRead()
        {
            Given(
                this.SetupContainer(),
                true,
                new List<ICommand> { AccountTests.SignUp("peter@oexenhave.dk") },
                db => Assert.Equal(1, db.Accounts.Count()));
        }
    }
}
