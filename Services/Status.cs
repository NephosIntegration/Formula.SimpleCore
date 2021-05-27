using System;
using System.Collections.Generic;
using System.Linq;

namespace Formula.SimpleCore
{
    public class Status<TData> : StatusBase<TData>
    {

    }
        
    public class StatusBase<TData>
    {
        public StatusBase()
        {
            this.Reset();
        }

        public StatusBase<TData> Reset()
        {
            return this.Succeed()
                       .SetMessage(null)
                       .SetData(default(TData))
                       .SetDetails(null);
        }


        public Boolean IsSuccessful { get; set; }

        public StatusBase<TData> Succeed()
        {
            this.IsSuccessful = true;
            return this;
        }

        public StatusBase<TData> Fail()
        {
            this.IsSuccessful = false;
            return this;
        }

        public StatusBase<TData> SetIsSuccessful(Boolean isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
            return this;
        }



        public String Message { get; set; }

        public StatusBase<TData> SetMessage(String message)
        {
            this.Message = message;
            return this;
        }



        public TData Data { get; set; }
        public StatusBase<TData> SetData(TData data)
        {
            this.Data = data;
            return this;
        }
        public TData GetData()
        {
            return this.Data;
        }


        public Dictionary<String, String> Details { get; set; }
        public StatusBase<TData> SetDetails(Dictionary<String, String> details)
        {
            this.Details = details;
            return this;
        }

        public StatusBase<TData> RecordFailure(String message, String subject = null)
        {
            this.Fail().SetMessage(message);

            if (String.IsNullOrEmpty(subject) == false)
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

        public StatusBase<TNewData> ConvertWithDataAs<TNewData>(TNewData newData)
        {
            var newBuilder = new StatusBase<TNewData>();

            newBuilder.SetIsSuccessful(this.IsSuccessful)
                      .SetMessage(this.Message)
                      .SetData(newData)
                      .SetDetails(this.Details);

            return newBuilder;
        }

        public StatusBase<TNewData> ConvertTo<TNewData>()
        {
            var newData = (TNewData)(object)this.Data; // (TNewData)Convert.ChangeType(this.Data, typeof(TNewData));
            return this.ConvertWithDataAs<TNewData>(newData);
        }

    }
}
