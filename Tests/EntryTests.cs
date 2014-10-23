
namespace Medlars.Tests
{
    using System;
    using System.Collections.Generic;

    using Medlars.Command.Account;
    using Medlars.Command.Entry;

    using TastyDomainDriven;

    using Xunit;

    public class EntryTests : BaseTest
    {
        private readonly AccountId accountId;

        public EntryTests()
        {
            this.accountId = new AccountId(new Guid("31C69AFF-BCFD-47AB-820E-799341F7F822"));
        }

        [Fact]
        public void AddSimpleStringEntryWithCorrectIp()
        {
            this.Given(
                this.SetupContainer(),
                new List<ICommand>
                {
                    this.SignUp(),
                    this.AddString()
                },
                (eventStore) =>
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
                    this.SignUp()
                },
                (bus) => Assert.Throws<HashInvalidException>(() => bus.Dispatch(this.AddString(userHostAddress: "8.8.8.8"))));
        }

        [Fact]
        public void AddSimpleStringEntryWithoutAccount()
        {
            this.Given(
                this.SetupContainer(),
                new List<ICommand>(),
                (bus) => Assert.Throws<AccoutMissingException>(() => bus.Dispatch(this.AddString())));
        }

        private ICommand SignUp(
            string email = "soeren@oexenhave.dk")
        {
            return new SignUpCommand
            {
                Email = email,
                Id = accountId,
                Timestamp = DateTime.Now
            };
        }

        private ICommand AddString(
            string hash = "invalidhash",
            string message = "Default message",
            string service = "DefaultService",
            string userHostAddress = "127.0.0.1")
        {
            return new AddStringEntryCommand
            {
                AccountId = accountId,
                Hash = hash,
                Id = new EntryId(new Guid("9BB603CA-B13B-4AB8-A380-6C19BB776E0E")),
                Message = message,
                Service = service,
                Severity = EntrySeverity.Debug,
                Timestamp = DateTime.Now,
                UserHostAddress = userHostAddress
            };
        }
    }
}
