using System;
using System.Collections.Generic;
using System.Linq;

namespace Depra.Data.Application.Storage.SupportedTypes
{
    public class SupportedTypes : ISupportedTypes
    {
        private readonly Type[] _types;

        public bool MathTypes(IEnumerable<Type> supportedTypes) => _types.Any(supportedTypes.Contains);

        public SupportedTypes(params Type[] types)
        {
            _types = types;
        }
    }
}