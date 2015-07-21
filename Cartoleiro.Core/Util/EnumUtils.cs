using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Util
{
    public static class EnumUtils
    {
        public static IEnumerable<T> TodosOsItens<T>()
        {
            return Enum.GetValues(typeof(T)).OfType<T>();
        }
    }
}
