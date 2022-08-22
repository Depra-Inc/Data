using System;
using System.Collections.Generic;

namespace Depra.Data.Storage.Internal.SupportedTypes
{
    public interface ISupportedTypes
    {
        bool MathTypes(IEnumerable<Type> supportedTypes);
    }
}