using System;

namespace XCommon.Patterns.Ioc
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class InjectAttribute : Attribute
    {
        public InjectAttribute(bool canCache = true, bool forceResolve = true)
        {
            ForceResolve = forceResolve;
            CanCache = canCache;
        }

        public bool ForceResolve { get; internal set; }

        public bool CanCache { get; internal set; }
    }
}
