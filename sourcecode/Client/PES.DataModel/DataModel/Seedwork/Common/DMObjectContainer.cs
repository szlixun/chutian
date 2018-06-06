using System;
using PES.DataModel.Helpers;
using PES.DataModel.IoC;
using PES.DataModel.IoC.TinyIoC;

namespace PES.DataModel
{
    #region IObjectContainer

    internal static class DMObjectContainer
    {
        private static IObjectContainer container;

        static DMObjectContainer()
        {
            SetCurrentContainer(new TinyObjectContainer());
        }

        public static AbstractProvider GetProvider()
        {
            return container.Resolve<AbstractProvider>();
        }

        public static AbstractTranslator GetTSQLTranslator()
        {
            return container.Resolve<AbstractTranslator>();
        }

        public static bool IsRegistered(Type type)
        {
            return container.IsRegistered(type);
        }

        public static void Register<TService, TImpl>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService
        {
            container.RegisterType<TService, TImpl>(life);
        }

        public static void RegisterAllDefault(IObjectContainer objectContainer)
        {
            RegisterProvider();
        }

        public static void RegisterProvider()
        {
            switch (DMConfiguration.ProviderType)
            {
                case EnumProviderType.MySql:
                    Register<AbstractProvider, MySqlDbProvider>();
                    Register<AbstractTranslator, MySqlExpressionTSQLTranslator>(LifeStyle.Transient);
                    break;

                case EnumProviderType.MsSql:

                    Register<AbstractProvider, MsSqlDbProvider>();
                    Register<AbstractTranslator, MsSqlExpressionTSQLTranslator>(LifeStyle.Transient);
                    break;

                case EnumProviderType.Access:

                    Register<AbstractProvider, AccessDbProvider>();
                    Register<AbstractTranslator, AccessExpressionTSQLTranslator>(LifeStyle.Transient);
                    break;

                default:

                    Register<AbstractProvider, MsSqlDbProvider>();
                    Register<AbstractTranslator, MsSqlExpressionTSQLTranslator>(LifeStyle.Transient);
                    break;
            }
        }

        public static T Resolve<T>() where T : class
        {
            return container.Resolve<T>();
        }

        public static void SetCurrentContainer(IObjectContainer objectContainer)
        {
            container = objectContainer;
            RegisterAllDefault(objectContainer);
        }
    }

    #endregion IObjectContainer
}