using System;

namespace EchoServer.ScreenConsole
{
    public class HttpEvent
    {
        public HttpEvent(int id, string operation, DateTime datetime, string data)
        {
            this.Id = id;
            this.Operation = operation;
            this.Data = data;
            this.DateTime = datetime;
        }

        public int Id { get; }
        public string Operation { get; }
        public string Data { get; }
        public DateTime DateTime { get; }
    }
}
