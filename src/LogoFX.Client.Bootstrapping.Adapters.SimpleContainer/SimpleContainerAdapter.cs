using System;
using System.Collections.Generic;
using LogoFX.Client.Bootstrapping.Adapters.Contracts;
using Solid.Practices.IoC;

namespace LogoFX.Client.Bootstrapping.Adapters.SimpleContainer
{
    /// <summary>
    /// Represents implementation of IoC container and bootstrapper adapter using Simple Container
    /// </summary>
    public class SimpleContainerAdapter : IIocContainer, IIocContainerAdapter<Practices.IoC.SimpleContainer>,
        IIocContainerScoped, IBootstrapperAdapter
    {
        private readonly Practices.IoC.SimpleContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleContainerAdapter"/> class.
        /// </summary>
        public SimpleContainerAdapter()
            :this(new Practices.IoC.SimpleContainer())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleContainerAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public SimpleContainerAdapter(Practices.IoC.SimpleContainer container)
        {
            _container = container;
            _container.RegisterSingleton(typeof(Practices.IoC.SimpleContainer), null, typeof(Practices.IoC.SimpleContainer));
        }

        /// <summary>
        /// Registers service in transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        public void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterPerRequest(typeof(TService), null, typeof(TImplementation));
        }

        /// <summary>
        /// Registers service in transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        public void RegisterTransient<TService>() where TService : class
        {
            RegisterTransient<TService, TService>();
        }

        /// <summary>
        /// Registers service in transient lifetime style.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        public void RegisterTransient(Type serviceType, Type implementationType)
        {
            _container.RegisterPerRequest(serviceType, null, implementationType);
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterTransient<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterTransient<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration.</param>
        /// <param name="implementationType">Type of dependency implementation.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterTransient(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterHandler(serviceType, null, (container, args) => dependencyCreator);
        }

        /// <summary>
        /// Registers service in singleton lifetime style.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        public void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService
        {
            RegisterSingletonImpl(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        public void RegisterSingleton<TService>() where TService : class
        {
            RegisterSingletonImpl(typeof(TService), typeof(TService));
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            RegisterSingletonImpl(serviceType, implementationType);
        }

        private void RegisterSingletonImpl(Type serviceType, Type implementationType)
        {
            _container.RegisterSingleton(serviceType, null, implementationType);
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterSingleton<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterSingleton<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration.</param>
        /// <param name="implementationType">Type of dependency implementation.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterSingleton(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterHandler(serviceType, null, (container, args) => dependencyCreator);
        }

        /// <summary>
        /// Registers the instance of the service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="instance">The instance.</param>
        public void RegisterInstance<TService>(TService instance) where TService : class
        {
            RegisterInstanceImpl(typeof(TService), instance);
        }

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="dependencyType">Type of the dependency.</param>
        /// <param name="instance">The instance.</param>
        public void RegisterInstance(Type dependencyType, object instance)
        {
            RegisterInstanceImpl(dependencyType, instance);
        }

        private void RegisterInstanceImpl(Type dependencyType, object instance)
        {
            _container.RegisterInstance(dependencyType, null, instance);
        }

        /// <summary>
        /// Registers the dependency via the handler.
        /// </summary>
        /// <param name="dependencyType">Type of the dependency.</param><param name="handler">The handler.</param>
        public void RegisterHandler(Type dependencyType, Func<object> handler)
        {
            _container.RegisterHandler(dependencyType, null, (container, args) => handler());
        }

        /// <summary>
        /// Registers the dependency via the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void RegisterHandler<TService>(Func<TService> handler) where TService : class
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => handler());
        }

        /// <summary>
        /// Registers the collection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="dependencyTypes">The dependency types.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RegisterCollection<TService>(IEnumerable<Type> dependencyTypes) where TService : class
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterSingleton(typeof (TService), null, type);
            }
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="dependencies">The dependencies.</param>
        public void RegisterCollection<TService>(IEnumerable<TService> dependencies) where TService : class
        {
            _container.RegisterInstance(typeof(IEnumerable<TService>),null, dependencies);
        }

        /// <summary>
        /// Registers the collection.
        /// </summary>
        /// <param name="dependencyType">Type of the dependency.</param>
        /// <param name="dependencyTypes">The dependency types.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RegisterCollection(Type dependencyType, IEnumerable<Type> dependencyTypes)
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterSingleton(dependencyType, null, type);
            }
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <param name="dependencyType">The dependency type.</param>
        /// <param name="dependencies">The dependencies.</param>
        public void RegisterCollection(Type dependencyType, IEnumerable<object> dependencies)
        {
            foreach (var dependency in dependencies)
            {
                _container.RegisterInstance(dependencyType, null, dependency);
            }
        }

        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <param name="lifetimeProvider">The lifetime scope.</param>
        /// <param name="service">The service.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterScoped(Func<object> lifetimeProvider, Type service, Type implementation)
        {
            _container.RegisterPerLifetime(lifetimeProvider, service, null, implementation);
        }

        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetimeProvider">The lifetime provider.</param>
        public void RegisterScoped<TService, TImplementation>(Func<object> lifetimeProvider)
        {
            _container.RegisterPerLifetime(lifetimeProvider, typeof(TService), null, typeof(TImplementation));
        }

        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="lifetimeProvider">The lifetime provider.</param>
        public void RegisterScoped<TService>(Func<object> lifetimeProvider)
        {
            _container.RegisterPerLifetime(lifetimeProvider, typeof(TService), null, typeof(TService));
        }

        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public TService GetInstance<TService>(Type serviceType) where TService : class
        {
            return (TService)_container.GetInstance(serviceType, null);
        }

        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        public TService GetInstance<TService>() where TService : class
        {
            return GetInstance<TService>(typeof(TService));
        }

        /// <summary>
        /// Resolves an instance of required service by its type
        /// </summary>
        /// <param name="serviceType">Type of service</param>
        /// <param name="key">Optional service key</param>
        /// <returns>Instance of service</returns>
        public object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        /// <summary>
        /// Resolves all instances of required service by its type
        /// </summary>
        /// <param name="serviceType">Type of service</param>
        /// <returns>All instances of requested service</returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        /// <summary>
        /// Resolves instance's dependencies and injects them into the instance
        /// </summary>
        /// <param name="instance">Instance to get injected with dependencies</param>
        public void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        /// <summary>
        /// Resolves service.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService Resolve<TService>() where TService : class
        {
            return GetInstance<TService>();
        }

        /// <summary>
        /// Resolves service.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object Resolve(Type serviceType)
        {
            return GetInstance(serviceType, null);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>        
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
