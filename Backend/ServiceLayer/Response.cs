using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    ///<summary>Class <c>Response</c> represents the result of a call to a void function. 
    ///If an exception was thrown, <c>ErrorOccured = true</c> and <c>ErrorMessage != null</c>. 
    ///Otherwise, <c>ErrorOccured = false</c> and <c>ErrorMessage = null</c>.</summary>
    public class Response
    {
        public string ErrorMessage { get; }

        [JsonProperty("ReturnValue", NullValueHandling = NullValueHandling.Ignore)]
        public object ReturnValue { get; }

        public Response(object val)
        {
            ReturnValue = val;
        }
        public Response(string msg, bool err)
        {
            if (err)
                ErrorMessage = msg;
        }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }
        public bool ErrorOccured()
        {
            return ErrorMessage != null;
        }
    }
}


/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response
    {
        public bool ErrorOcuured { get => ErrorOcuured != null; }
        public readonly string ErrorMessage;
        internal Response() { }
        internal Response(string massage)
        {
            this.ErrorMessage = massage;
        }
    }
    public class Response<T> : Response
    {
        public readonly T Value;
        private Response(T value, string msg) : base(msg)
        {
            this.Value = value;
        }

        internal static Response<T> FromValue(T value)
        {
            return new Response<T>(value, null);
        }

        internal static Response<T> FromError(string msg)
        {
            return new Response<T>(default(T), msg);
        }
    }
}*/