using System;

namespace PES.DataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DMIgnoreAttribute : Attribute
    {
        public DMIgnoreAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class DMTableAttribute : Attribute
    {
        public DMTableAttribute(string name, string primaryKey, bool isIdentity)
        {
            this.Name = name;
            this.PrimaryKey = primaryKey;
            this.IsIdentity = isIdentity;
        }

        public DMTableAttribute(string name, string primaryKey, bool isIdentity, bool isUseCustomConnection)
        {
            this.Name = name;
            this.PrimaryKey = primaryKey;
            this.IsIdentity = isIdentity;
            this.IsUseCustomConnection = isUseCustomConnection;
        }

        public DMTableAttribute(string name, string primaryKey, bool isIdentity, string connectionKey)
        {
            this.Name = name;
            this.PrimaryKey = primaryKey;
            this.IsIdentity = isIdentity;
            this.IsUseCustomConnection = true;
            this.ConnectionKey = connectionKey;
        }

        public DMTableAttribute()
        {
        }

        public string ConnectionKey { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsUseCustomConnection { get; set; }

        public string Name { get; set; }

        public string PrimaryKey { get; set; }
    }
}