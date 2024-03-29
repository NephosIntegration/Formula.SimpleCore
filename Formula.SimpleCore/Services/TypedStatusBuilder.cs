using System;
using System.Collections.Generic;

namespace Formula.SimpleCore
{

    [Obsolete("Move to Status instead of TypedStatusBuilder")]
    public class TypedStatusBuilder<TData>
    {
        public TypedStatusBuilder()
        {
            this.Reset();
        }

        public TypedStatusBuilder<TData> Reset()
        {
            return this.Succeed()
                       .SetMessage(null)
                       .SetData(default(TData))
                       .SetDetails(null);
        }


        public bool IsSuccessful { get; set; }

        public TypedStatusBuilder<TData> Succeed()
        {
            this.IsSuccessful = true;
            return this;
        }

        public TypedStatusBuilder<TData> Fail()
        {
            this.IsSuccessful = false;
            return this;
        }

        public TypedStatusBuilder<TData> SetIsSuccessful(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
            return this;
        }



        public string Message { get; set; }

        public TypedStatusBuilder<TData> SetMessage(string message)
        {
            this.Message = message;
            return this;
        }



        public TData Data { get; set; }
        public TypedStatusBuilder<TData> SetData(TData data)
        {
            this.Data = data;
            return this;
        }
        public TData GetData()
        {
            return this.Data;
        }


        public Dictionary<string, string> Details { get; set; }
        public TypedStatusBuilder<TData> SetDetails(Dictionary<string, string> details)
        {
            this.Details = details;
            return this;
        }

        public TypedStatusBuilder<TData> RecordFailure(string message, string subject = null)
        {
            this.Fail().SetMessage(message);

            if (string.IsNullOrEmpty(subject) == false)
            {
                if (this.Details == null)
                {
                    this.Details = new Dictionary<string, string>();
                }

                if (this.Details.ContainsKey(subject))
                {
                    this.Details[subject] = message;
                }
                else
                {
                    this.Details.Add(subject, message);
                }
            }

            return this;
        }

        public TypedStatusBuilder<TNewData> ConvertWithDataAs<TNewData>(TNewData newData)
        {
            var newBuilder = new TypedStatusBuilder<TNewData>();

            newBuilder.SetIsSuccessful(this.IsSuccessful)
                      .SetMessage(this.Message)
                      .SetData(newData)
                      .SetDetails(this.Details);

            return newBuilder;
        }

        public TypedStatusBuilder<TNewData> ConvertTo<TNewData>()
        {
            var newData = (TNewData)(object)this.Data; // (TNewData)Convert.ChangeType(this.Data, typeof(TNewData));
            return this.ConvertWithDataAs<TNewData>(newData);
        }

    }
}
