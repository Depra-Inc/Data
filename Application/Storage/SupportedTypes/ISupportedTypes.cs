using System;
using System.Collections.Generic;

namespace Depra.Data.Application.Storage.SupportedTypes
{
    public interface ISupportedTypes
    {
        bool MathTypes(IEnumerable<Type> supportedTypes);
    }
}