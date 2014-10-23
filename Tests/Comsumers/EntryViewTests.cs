namespace Medlars.Tests.Comsumers
{
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;

    using Medlars.Command.Account;
    using Medlars.Command.Entry;
    using Medlars.Query.Consumers.Database;
    using Medlars.Tests.Commands;

    using TastyDomainDriven;

    using Xunit;

    public class EntryViewTests : BaseQueryTest
    {
        public override void RegisterTestTypes(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().AsImplementedInterfaces();
            builder.RegisterType<EntryService>().AsImplementedInterfaces();
            builder.RegisterType<AccountView>().AsImplementedInterfaces();
            builder.RegisterType<EntryView>().AsImplementedInterfaces();
        }

        [Fact]
        public void CreateStringEntryForRead()
        {
            Given(
                this.SetupContainer(),
                true,
                new List<ICommand> { AccountTests.SignUp(), EntryTests.AddString() },
                db => Assert.Equal(1, db.Entries.Count()));
        }

    }
}
