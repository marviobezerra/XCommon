using System;
using System.Reflection;

namespace XCommon.Util
{
    public class AttributeDetail<T>
        where T : Attribute
    {
        public PropertyInfo Property { get; set; }

        public T Attribute { get; set; }
    }
}
