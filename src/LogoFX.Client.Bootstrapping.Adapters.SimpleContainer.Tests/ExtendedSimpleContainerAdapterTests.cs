using FluentAssertions;
using LogoFX.Client.Bootstrapping.Adapters.SimpleContainer;
using LogoFX.Practices.IoC;
using Xunit;

namespace LogoFX.Client.Bootstrapping.Tests
{    
    public class ExtendedSimpleContainerAdapterTests
    {
        [Fact]
        public void Given_WhenDependencyIsRegisteredPerLifetimeAndDependencyIsResolved_ThenResolvedDependencyIsNotNull()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterPerLifetime<ITestDependency, TestDependency>(() => TestLifetimeScopeProvider.Current);
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency = container.Resolve<ITestDependency>();

            dependency.Should().NotBeNull();            
        }

        [Fact]
        public void Given_WhenDependencyIsRegisteredPerLifetimeAndDependencyIsResolvedAndLifetimeChangesAndDependencyIsResolved_ThenResolvedDependenciesAreDifferent()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterPerLifetime<ITestDependency, TestDependency>(() => TestLifetimeScopeProvider.Current);
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency1 = container.Resolve<ITestDependency>();
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency2 = container.Resolve<ITestDependency>();

            dependency1.Should().NotBeSameAs(dependency2);            
        }

        [Fact]
        public void Given_WhenDependencyIsRegisteredPerLifetimeAndDependencyIsResolvedAndLifetimeIsSetToNullAndDependencyIsResolved_ThenResolvedDependencyIsNull()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterPerLifetime<ITestDependency, TestDependency>(() => TestLifetimeScopeProvider.Current);
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency = container.Resolve<ITestDependency>();
            TestLifetimeScopeProvider.Current = null;
            dependency = container.Resolve<ITestDependency>();

            dependency.Should().BeNull();            
        }

        [Fact]
        public void Given_WhenDependencyIsRegisteredViaHandlerAndDependencyIsResolved_ThenResolvedDependencyIsNotNull()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterHandler<ITestDependency>(() => new TestDependency());
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency = container.Resolve<ITestDependency>();

            dependency.Should().NotBeNull();            
        }

        [Fact]
        public void Given_WhenDependencyIsRegisteredViaHandlerAndDependencyIsResolvedTwice_ThenResolvedDependenciesAreDifferent()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterHandler<ITestDependency>(() => new TestDependency());            
            var dependencyOne = container.Resolve<ITestDependency>();
            var dependencyTwo = container.Resolve<ITestDependency>();

            dependencyOne.Should().NotBeSameAs(dependencyOne);            
        }
    }

    interface ICustomDependency
    {

    }

    class TestDependencyA : ICustomDependency
    {

    }

    class TestDependencyB : ICustomDependency
    {

    }
}
