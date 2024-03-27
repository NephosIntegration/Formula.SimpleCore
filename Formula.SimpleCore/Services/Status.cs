using System.Collections.Generic;

namespace Formula.SimpleCore
{
    public class Status<TData>
    {
        public Status()
        {
            this.Reset();
        }

        public Status<TData> Reset()
        {
            return this.Succeed()
                       .SetMessage(null)
                       .SetData(default(TData))
                       .SetDetails(null);
        }


        public bool IsSuccessful { get; set; }

        public Status<TData> Succeed()
        {
            this.IsSuccessful = true;
            return this;
        }

        public Status<TData> Fail()
        {
            this.IsSuccessful = false;
            return this;
        }

        public Status<TData> SetIsSuccessful(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
            return this;
        }



        public string Message { get; set; }

        public Status<TData> SetMessage(string message)
        {
            this.Message = message;
            return this;
        }



        public TData Data { get; set; }
        public Status<TData> SetData(TData data)
        {
            this.Data = data;
            return this;
        }
        public TData GetData()
        {
            return this.Data;
        }


        public Dictionary<string, string> Details { get; set; }
        public Status<TData> SetDetails(Dictionary<string, string> details)
        {
            this.Details = details;
            return this;
        }

        public Status<TData> RecordFailure(string message, string subject = null)
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

        public Status<TNewData> ConvertWithDataAs<TNewData>(TNewData newData)
        {
            var newBuilder = new Status<TNewData>();

            newBuilder.SetIsSuccessful(this.IsSuccessful)
                      .SetMessage(this.Message)
                      .SetData(newData)
                      .SetDetails(this.Details);

            return newBuilder;
        }

        public Status<TNewData> ConvertTo<TNewData>()
        {
            var newData = (TNewData)(object)this.Data; // (TNewData)Convert.ChangeType(this.Data, typeof(TNewData));
            return this.ConvertWithDataAs<TNewData>(newData);
        }

    }
}
