using System;

namespace Samola.EchoServer.ScreenConsole.Components.EventsComponent
{
    /// <summary>
    /// A single HTTP event
    /// PURPOSE:
    ///     Encapsulates a single event's data
    /// </summary>
    public class Event
    {
        public Event(string operation, DateTime datetime, string data)
        {
            this.Operation = operation;
            this.DateTime = datetime;
            this.Data = data;
        }

        public string Operation { get; }
        public DateTime DateTime { get; }
        public string Data { get; }
    }
}