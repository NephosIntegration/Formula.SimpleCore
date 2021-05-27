using System;
using System.Collections.Generic;
using System.Linq;

namespace Formula.SimpleCore
{

    public static class StatusBuilderExtensions
    {
        [Obsolete("Move to Status instead of TypedStatusBuilder")]
        public static Status<TData> ToStatus<TData>(this StatusBuilder legacy)
        {
            var status = new Status<TData>();

            status.SetIsSuccessful(legacy.IsSuccessful)
                      .SetMessage(legacy.Message)
                      .SetData(legacy.GetDataAs<TData>())
                      .SetDetails(legacy.Details);

            return status;
        }
    }

    [Obsolete("Move to Status instead of TypedStatusBuilder")]
    public class StatusBuilder : TypedStatusBuilder<Object>
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
