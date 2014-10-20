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

    public class AccountTests
    {
        [Fact]
        public void SignupCommandSuccess()
        {
            var startup = new DependencyInjectionStartup();
            var scope = startup.GetContainer().BeginLifetimeScope();
            var bus = scope.Resolve<IBus>();

            bus.Dispatch(new SignUpCommand { Email = "soeren@oexenhave.dk", Id = new AccountId(Guid.NewGuid()), Timestamp = DateTime.Now });
            Assert.True(true);
        }
    }
}
