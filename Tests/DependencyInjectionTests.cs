namespace Medlars.Tests
{
    using Autofac;

    using Medlars.Website;

    using TastyDomainDriven;

    using Xunit;

    public class DependencyInjectionTests
    {
        [Fact]
        public void StartTest()
        {
            var startup = new DependencyInjectionStartup();
            var scope = startup.GetContainer().BeginLifetimeScope();
            scope.Resolve<IBus>();
        }
    }
}