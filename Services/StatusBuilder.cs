using System;
using System.Collections.Generic;
using System.Linq;

namespace Formula.SimpleCore
{
    public class StatusBuilder : StatusBuilderTyped<Object>
    {
        public StatusBuilder() : base()
        {
        }

        public TData GetDataAs<TData>()
        {
            return (TData)this.Data;
        }
    }
}
