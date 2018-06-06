using System;

namespace PES.DataModel.IoC
{
    #region IObjectContainer

    public enum LifeStyle
    {
        Transient,
        Singleton
    }

    public interface IObjectContainer
    {
        object Current { get; }

        bool IsRegistered(Type type);

        void RegisterType<TFrom, TTo>(LifeStyle life)
            where TFrom : class
            where TTo : class, TFrom;

        void RegisterInstance<TType>(TType instance, LifeStyle life)
            where TType : class;

        TType Resolve<TType>() where TType : class;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public ComponentAttribute()
            : this(LifeStyle.Transient)
        {
        }

        public ComponentAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }

        public LifeStyle LifeStyle { get; private set; }
    }

    #endregion IObjectContainer
}