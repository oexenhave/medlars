
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

        private IEnumerable<ICommand> SignUp()
        {
            yield return new SignUpCommand
            {
                Email = "soeren@oexenhave.dk",
                Id = accountId,
                Timestamp = DateTime.Now
            };

            yield return new AddStringEntryCommand
                         {
                             AccountId = accountId,
                             Hash = "asdasdasd",
                             Id = new EntryId(new Guid("9BB603CA-B13B-4AB8-A380-6C19BB776E0E")),
                             Message = "New log",
                             Service = "MyService",
                             Severity = EntrySeverity.Debug,
                             Timestamp = DateTime.Now,
                             UserHostAddress = "127.0.0.1"
                         };
        }

        [Fact]
        public void AddSimpleStringEntry()
        {
            this.Given(
                this.SetupContainer(),
                this.SignUp(),
                (eventStore) =>
                {
                    var stream = eventStore.ReplayAll();
                    Assert.Equal(2, stream.Events.Count);
                });
        }

        [Fact]
        public void AddSimpleStringEntryWithoutAccount()
        {
            this.Given(
                this.SetupContainer(),
                this.SignUp(),
                (eventStore) =>
                {
                    var stream = eventStore.ReplayAll();
                    Assert.Equal(2, stream.Events.Count);
                });
        }
    }
}
