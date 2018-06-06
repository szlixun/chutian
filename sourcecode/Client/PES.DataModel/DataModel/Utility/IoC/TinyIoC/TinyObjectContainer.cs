using System;

namespace PES.DataModel.IoC.TinyIoC
{
    #region TinyIoC

    public static class TinyIoCContainerExtensions
    {
        public static TinyIoCContainer.RegisterOptions Life(this TinyIoCContainer.RegisterOptions registration, LifeStyle life)
        {
            if (life == LifeStyle.Singleton)
            {
                return registration.AsSingleton();
            }
            return registration.AsMultiInstance();
        }
    }

    public class TinyObjectContainer : IObjectContainer
    {
        private TinyIoCContainer container;

        public TinyObjectContainer()
        {
            container = new TinyIoCContainer();
        }

        public bool IsRegistered(Type type)
        {
            return container.CanResolve(type);
        }

        public void RegisterType<TFrom, TTo>(LifeStyle life)
            where TFrom : class
            where TTo : class, TFrom
        {
            container.Register<TFrom, TTo>().Life(life);
        }

        public void RegisterInstance<TType>(TType instance, LifeStyle life)
            where TType : class
        {
            container.Register<TType>(instance).Life(life);
        }

        public TType Resolve<TType>() where TType : class
        {
            return container.Resolve<TType>();
        }

        public object Current
        {
            get { return this.container; }
        }
    }

    #endregion TinyIoC
}