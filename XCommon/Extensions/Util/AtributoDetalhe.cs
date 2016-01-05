using System;
using System.Reflection;

namespace XCommon.Extensions.Util
{
    public class AtributoDetalhe<T>
        where T : Attribute
    {
        public PropertyInfo Propriedade { get; set; }

        public T Atributo { get; set; }
    }
}
