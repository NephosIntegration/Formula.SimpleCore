//using System;
//using System.Collections.Generic;

//namespace Formula.SimpleCore
//{

//    [Obsolete("Move to Status instead of TypedStatusBuilder")]
//    public class TypedStatusBuilder<TData>
//    {
//        public TypedStatusBuilder()
//        {
//            Reset();
//        }

//        public TypedStatusBuilder<TData> Reset()
//        {
//            return Succeed()
//                       .SetMessage(null)
//                       .SetData(default(TData))
//                       .SetDetails(null);
//        }


//        public bool IsSuccessful { get; set; }

//        public TypedStatusBuilder<TData> Succeed()
//        {
//            IsSuccessful = true;
//            return this;
//        }

//        public TypedStatusBuilder<TData> Fail()
//        {
//            IsSuccessful = false;
//            return this;
//        }

//        public TypedStatusBuilder<TData> SetIsSuccessful(bool isSuccessful)
//        {
//            IsSuccessful = isSuccessful;
//            return this;
//        }



//        public string Message { get; set; }

//        public TypedStatusBuilder<TData> SetMessage(string message)
//        {
//            Message = message;
//            return this;
//        }



//        public TData Data { get; set; }
//        public TypedStatusBuilder<TData> SetData(TData data)
//        {
//            Data = data;
//            return this;
//        }
//        public TData GetData()
//        {
//            return Data;
//        }


//        public Dictionary<string, string> Details { get; set; }
//        public TypedStatusBuilder<TData> SetDetails(Dictionary<string, string> details)
//        {
//            Details = details;
//            return this;
//        }

//        public TypedStatusBuilder<TData> RecordFailure(string message, string subject = null)
//        {
//            Fail().SetMessage(message);

//            if (string.IsNullOrEmpty(subject) == false)
//            {
//                if (Details == null)
//                {
//                    Details = new Dictionary<string, string>();
//                }

//                if (Details.ContainsKey(subject))
//                {
//                    Details[subject] = message;
//                }
//                else
//                {
//                    Details.Add(subject, message);
//                }
//            }

//            return this;
//        }

//        public TypedStatusBuilder<TNewData> ConvertWithDataAs<TNewData>(TNewData newData)
//        {
//            var newBuilder = new TypedStatusBuilder<TNewData>();

//            newBuilder.SetIsSuccessful(IsSuccessful)
//                      .SetMessage(Message)
//                      .SetData(newData)
//                      .SetDetails(Details);

//            return newBuilder;
//        }

//        public TypedStatusBuilder<TNewData> ConvertTo<TNewData>()
//        {
//            var newData = (TNewData)(object)Data; // (TNewData)Convert.ChangeType(this.Data, typeof(TNewData));
//            return ConvertWithDataAs<TNewData>(newData);
//        }

//    }
//}
