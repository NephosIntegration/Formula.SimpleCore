using System;
using System.Collections.Generic;
using System.Linq;

namespace Formula.SimpleCore
{
    public static class TypedStatusBuilderExtensions
    {
        [Obsolete("Move to Status instead of TypedStatusBuilder")]
        public static Status<TData> ToStatus<TData>(this TypedStatusBuilder<TData> legacy)
        {
            var status = new Status<TData>();

            status.SetIsSuccessful(legacy.IsSuccessful)
                      .SetMessage(legacy.Message)
                      .SetData(legacy.Data)
                      .SetDetails(legacy.Details);

            return status;
        }
    }

    [Obsolete("Move to Status instead of TypedStatusBuilder")]
    public class TypedStatusBuilder<TData> : StatusBase<TData>
    {

    }
}
