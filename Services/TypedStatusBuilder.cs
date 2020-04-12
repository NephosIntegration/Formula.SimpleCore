using System;
using System.Collections.Generic;
using System.Linq;

namespace Formula.SimpleCore
{
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


        public Boolean IsSuccessful { get; set; }

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



        public String Message { get; set; }

        public TypedStatusBuilder<TData> SetMessage(String message)
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


        public Dictionary<String, String> Details { get; set; }
        public TypedStatusBuilder<TData> SetDetails(Dictionary<String, String> details)
        {
            this.Details = details;
            return this;
        }

        public TypedStatusBuilder<TData> RecordFailure(String message, String subject = null)
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

    }
}
