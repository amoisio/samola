using EchoServer.ScreenConsole.Components;
using EchoServer.ScreenConsole.Components.EventsComponent;
using EchoServer.ScreenConsole.Renderer;
using System;
using System.Threading;

namespace EchoServer.ScreenConsole
{
    class Program
    {
        static EventsComponent events1;
        static EventsComponent events2;
        static void Main(string[] args)
        {
            var drawArea = new DrawArea(0, 0, 40, 20);
            var renderer = new ConsoleRenderer(drawArea);
            events1 = new EventsComponent("A test title", renderer);
            events1.Initialize();

            var drawArea2 = new DrawArea(41, 0, 40, 20);
            var renderer2 = new ConsoleRenderer(drawArea2);
            events2 = new EventsComponent("Another test title", renderer2);
            events2.Initialize();

            var t1 = new Thread(DoEvents1);
            var t2 = new Thread(DoEvents2);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
            //int i = 200;
            //while (i-- > 0)
            //{
            //    events.Register(new Event("GET", DateTime.UtcNow, Guid.NewGuid().ToString()));
            //    events.Refresh();

            //    Thread.Sleep(100);
            //}
        }

        static void DoEvents1()
        {
            int i = 200;
            while (i-- > 0)
            {
                events1.Register(new Event("GET", DateTime.UtcNow, Guid.NewGuid().ToString()));
                events1.Refresh();

                Thread.Sleep(100);
            }
        }

        static void DoEvents2()
        {
            int i = 600;
            while (i-- > 0)
            {
                events2.Register(new Event("POST", DateTime.UtcNow, Guid.NewGuid().ToString()));
                events2.Refresh();

                Thread.Sleep(30);
            }
        }
    }
}
