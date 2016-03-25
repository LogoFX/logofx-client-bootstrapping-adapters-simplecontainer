using System.Collections.Generic;
using System.Linq;
using LogoFX.Client.Bootstrapping.Adapters.SimpleContainer;
using LogoFX.Practices.IoC;
using NUnit.Framework;

namespace LogoFX.Client.Bootstrapping.Tests
{
    [TestFixture]
    class ExtendedSimpleContainerAdapterTests
    {
        [Test]
        public void Given_WhenDependencyIsRegisteredPerLifetimeAndDependencyIsResolved_ThenResolvedDependencyIsNotNull()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterPerLifetime<ITestDependency, TestDependency>(() => TestLifetimeScopeProvider.Current);
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency = container.Resolve<ITestDependency>();

            Assert.IsNotNull(dependency);
        }

        [Test]
        public void Given_WhenDependencyIsRegisteredPerLifetimeAndDependencyIsResolvedAndLifetimeChangesAndDependencyIsResolved_ThenResolvedDependenciesAreDifferent()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterPerLifetime<ITestDependency, TestDependency>(() => TestLifetimeScopeProvider.Current);
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency1 = container.Resolve<ITestDependency>();
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency2 = container.Resolve<ITestDependency>();

            Assert.AreNotEqual(dependency1, dependency2);
        }

        [Test]
        public void Given_WhenDependencyIsRegisteredPerLifetimeAndDependencyIsResolvedAndLifetimeIsSetToNullAndDependencyIsResolved_ThenResolvedDependencyIsNull()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterPerLifetime<ITestDependency, TestDependency>(() => TestLifetimeScopeProvider.Current);
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency = container.Resolve<ITestDependency>();
            TestLifetimeScopeProvider.Current = null;
            dependency = container.Resolve<ITestDependency>();

            Assert.IsNull(dependency);
        }

        [Test]
        public void Given_WhenDependencyIsRegisteredViaHandlerAndDependencyIsResolved_ThenResolvedDependencyIsNotNull()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterHandler<ITestDependency>(() => new TestDependency());
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency = container.Resolve<ITestDependency>();

            Assert.IsNotNull(dependency);
        }

        [Test]
        public void Given_WhenDependencyIsRegisteredViaHandlerAndDependencyIsResolvedTwice_ThenResolvedDependenciesAreDifferent()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterHandler<ITestDependency>(() => new TestDependency());            
            var dependencyOne = container.Resolve<ITestDependency>();
            var dependencyTwo = container.Resolve<ITestDependency>();

            Assert.AreNotSame(dependencyTwo, dependencyOne);
        }
    }

    [TestFixture]
    class CollectionRegistrationTests
    {
        [Test]
        public void MultipleImplementationAreRegisteredByType_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new ExtendedSimpleContainerAdapter();
            adapter.RegisterCollection<ICustomDependency>(new[] { typeof(TestDependencyA), typeof(TestDependencyB) });

            var collection = adapter.Resolve<IEnumerable<ICustomDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            Assert.IsInstanceOf(typeof(TestDependencyA), firstItem);
            Assert.IsInstanceOf(typeof(TestDependencyB), secondItem);
        }

        [Test]
        public void MultipleImplementationAreRegisteredByInstance_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new ExtendedSimpleContainerAdapter();
            var instanceA = new TestDependencyA();
            var instanceB = new TestDependencyB();
            adapter.RegisterCollection(new ICustomDependency[] { instanceA, instanceB });

            var collection = adapter.Resolve<IEnumerable<ICustomDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            Assert.AreSame(instanceA, firstItem);
            Assert.AreSame(instanceB, secondItem);
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
