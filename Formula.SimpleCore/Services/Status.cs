using System.Collections.Generic;

namespace Formula.SimpleCore
{
    public class Status<TData>
    {
        public Status()
        {
            Reset();
        }

        public Status<TData> Reset()
        {
            return Succeed()
                       .SetMessage(null)
                       .SetData(default)
                       .SetDetails(null);
        }


        public bool IsSuccessful { get; set; }

        public Status<TData> Succeed()
        {
            IsSuccessful = true;
            return this;
        }

        public Status<TData> Fail()
        {
            IsSuccessful = false;
            return this;
        }

        public Status<TData> SetIsSuccessful(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
            return this;
        }

        public string Message { get; set; }

        public Status<TData> SetMessage(string message)
        {
            Message = message;
            return this;
        }

        public TData Data { get; set; }
        public Status<TData> SetData(TData data)
        {
            Data = data;
            return this;
        }

        public TData GetData()
        {
            return Data;
        }

        public Dictionary<string, string> Details { get; set; }

        public Status<TData> SetDetails(Dictionary<string, string> details)
        {
            Details = details;
            return this;
        }

        public Status<TData> RecordFailure(string message, string subject = null)
        {
            Fail().SetMessage(message);

            if (!string.IsNullOrWhiteSpace(subject))
            {
                Details ??= [];

                Details.TryAdd(subject, message);
            }

            return this;
        }

        public Status<TNewData> ConvertWithDataAs<TNewData>(TNewData newData)
        {
            var newBuilder = new Status<TNewData>();

            newBuilder.SetIsSuccessful(IsSuccessful)
                      .SetMessage(Message)
                      .SetData(newData)
                      .SetDetails(Details);

            return newBuilder;
        }

        public Status<TNewData> ConvertTo<TNewData>()
        {
            var newData = (TNewData)(object)Data; // (TNewData)Convert.ChangeType(this.Data, typeof(TNewData));
            return ConvertWithDataAs<TNewData>(newData);
        }

    }
}
