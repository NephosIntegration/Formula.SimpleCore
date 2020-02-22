using System;
using System.Collections.Generic;
using System.Linq;

namespace Formula.SimpleCore
{
    public class StatusBuilder
    {
        public StatusBuilder()
        {
            this.Reset();
        }

        public StatusBuilder Reset()
        {
            return this.Succeed()
                       .SetMessage(null)
                       .SetData(null)
                       .SetDetails(null);
        }


        public Boolean IsSuccessful { get; set; }

        public StatusBuilder Succeed()
        {
            this.IsSuccessful = true;
            return this;
        }

        public StatusBuilder Fail()
        {
            this.IsSuccessful = false;
            return this;
        }



        public String Message { get; set; }

        public StatusBuilder SetMessage(String message)
        {
            this.Message = message;
            return this;
        }



        public Object Data { get; set; }
        public StatusBuilder SetData(Object data)
        {
            this.Data = data;
            return this;
        }
        public TData GetDataAs<TData>()
        {
            return (TData)this.Data;
        }


        public Dictionary<String, String> Details { get; set; }
        public StatusBuilder SetDetails(Dictionary<String, String> details)
        {
            this.Details = details;
            return this;
        }

        public StatusBuilder RecordFailure(String message, String subject = null)
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
