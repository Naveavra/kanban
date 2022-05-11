using System;
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
}