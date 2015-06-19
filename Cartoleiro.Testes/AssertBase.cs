using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes
{
    public static class AssertBase
    {
        public static T Throws<T>(Action sutMetodo) where T : Exception
        {
            try
            {
                sutMetodo();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(T));
                return (T)ex;
            }

            throw new InvalidOperationException(string.Format("Teste não lançou exceção do tipo {0}.", typeof(T).Name));
        }
    }
}