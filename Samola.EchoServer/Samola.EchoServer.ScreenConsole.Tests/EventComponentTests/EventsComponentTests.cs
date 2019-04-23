using EchoServer.ScreenConsole.Renderer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EchoServer.ScreenConsole.Tests.TestSupport;
using EchoServer.ScreenConsole.Components.EventsComponent;
using EchoServer.ScreenConsole.Components;

namespace EchoServer.ScreenConsole.Tests.EventComponentTests
{
    public class An_EventsComponent
    {
        private FakeRenderer _renderer;
        private EventsComponent _eventsComponent;
        private string _title;
        private DrawArea _drawArea;

        public An_EventsComponent()
        {
            _drawArea = new DrawArea(new Point(0, 0), 40, 40);
            _renderer = new FakeRenderer(_drawArea);
            _title = "Test component";
            
            _eventsComponent = new EventsComponent(_title, _renderer);
        } 

        [Fact]
        public void Should_render_title_on_the_first_row_on_initialize()
        {
            _eventsComponent.Initialize();

            Assert.Equal(42, _renderer.RenderHistory.Count);
            Assert.Contains(_title, _renderer.RenderHistory[40].Item3);
            Assert.Equal(0, _renderer.RenderHistory[0].Item1);
            Assert.Equal(0, _renderer.RenderHistory[0].Item2);
        }

        [Fact]
        public void Should_render_header_on_the_second_row_on_initialize()
        {
            _eventsComponent.Initialize();

            Assert.Equal(42, _renderer.RenderHistory.Count);
            Assert.Contains("ID", _renderer.RenderHistory[41].Item3);
            Assert.Equal(0, _renderer.RenderHistory[1].Item1);
            Assert.Equal(1, _renderer.RenderHistory[1].Item2);
        }

        [Fact]
        public void Should_throw_if_registering_events_before_initialize()
        {
            try
            {
                _eventsComponent.Register(new Event("POST", DateTime.Now, "Test2"));
                _eventsComponent.Initialize();
                Assert.False(true);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
    }
}