namespace Medlars.Tests.Commands
{
    using System;
    using System.Collections.Generic;

    using Autofac;

    using Medlars.Command.Account;
    using Medlars.Command.Entry;

    using TastyDomainDriven;

    using Xunit;

    public class EntryTests : BaseCommandTest
    {
        public static ICommand AddString(
            string hash = "invalidhash",
            string message = "Default message",
            string service = "DefaultService",
            string userHostAddress = "127.0.0.1")
        {
            return new AddStringEntryCommand
                   {
                       AccountId = AccountTests.AccountId,
                       Hash = hash,
                       Id = new EntryId(new Guid("9BB603CA-B13B-4AB8-A380-6C19BB776E0E")),
                       Message = message,
                       Service = service,
                       Severity = EntrySeverity.Debug,
                       Timestamp = DateTime.Now,
                       UserHostAddress = userHostAddress
                   };
        }

        [Fact]
        public void AddSimpleStringEntryWithCorrectIp()
        {
            this.Given(
                this.SetupContainer(),
                new List<ICommand>
                {
                    AccountTests.SignUp(),
                    AddString()
                },
                eventStore =>
                {
                    var stream = eventStore.ReplayAll();
                    Assert.Equal(2, stream.Events.Count);
                });
        }

        [Fact]
        public void AddSimpleStringEntryWithIncorrectIp()
        {
            this.Given(
                this.SetupContainer(),
                new List<ICommand>
                {
                    AccountTests.SignUp()
                },
                bus => Assert.Throws<HashInvalidException>(() => bus.Dispatch(AddString(userHostAddress: "8.8.8.8"))));
        }

        [Fact]
        public void AddSimpleStringEntryWithoutAccount()
        {
            this.Given(
                this.SetupContainer(),
                new List<ICommand>(),
                bus => Assert.Throws<AccoutMissingException>(() => bus.Dispatch(AddString())));
        }

        public override void RegisterTestTypes(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().AsImplementedInterfaces();
            builder.RegisterType<EntryService>().AsImplementedInterfaces();
        }
    }
}
