using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer.Model
{
    public class ReturnMessage
    {
        public ReturnMessage(HttpRequest request, string message)
        {
            this.HttpVerb = request.Method;
            this.Protocol = request.Protocol;
            this.Path = request.Path;
            this.RemoteAddress = $"{request.HttpContext.Connection.RemoteIpAddress.ToString()}:{request.HttpContext.Connection.RemotePort}";
            this.LocalAddress = $"{request.HttpContext.Connection.LocalIpAddress.ToString()}:{request.HttpContext.Connection.LocalPort}";
            this.Response = $"Responding to '{message}'";
        }

        public string HttpVerb { get; set; }
        public string Protocol { get; set; }
        public string Path { get; set; }
        public string RemoteAddress { get; set; }
        public string LocalAddress { get; set; }
        public string Response { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.HttpVerb);
            sb.Append(" FROM:");
            sb.Append(this.RemoteAddress);
            sb.Append(" LOCAL:");
            sb.Append(this.LocalAddress);
            sb.Append(" RESPONSE:");
            sb.Append(this.Response);
            return sb.ToString();
        }
    }
}

