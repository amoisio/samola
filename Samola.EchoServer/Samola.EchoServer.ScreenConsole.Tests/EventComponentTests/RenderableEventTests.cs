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
    public class A_RenderableEvent
    {
        private FakeRenderer _renderer;
        private Event _event;
        private DrawArea _drawArea;

        public A_RenderableEvent()
        {
            _drawArea = new DrawArea(new Point(10, 5), 40, 25);
            _renderer = new FakeRenderer(_drawArea);
            _event = new Event("GET", DateTime.Parse("2019-01-01 00:00:00"), "Test");
        }

        [Fact]
        public void Should_be_dirty_after_initialization()
        {
            var renderable = new RenderableEvent(_event, _renderer, 1);

            bool isDirty = renderable.IsDirty();

            Assert.True(isDirty);
        }

        [Fact]
        public void Should_render_an_event_in_the_correct_place()
        {
            var renderable = new RenderableEvent(_event, _renderer, 1);

            renderable.Render(0);

            Assert.Equal(10, _renderer.X);
            Assert.Equal(5, _renderer.Y);
            Assert.Equal("1   19-01-01 00.00.00 GET  Test         ", _renderer.StringToRender);
            Assert.Equal(40, _renderer.StringToRender.Length);
        }

        [Fact]
        public void Should_omit_data_column_if_not_enough_space()
        {
            var drawArea = new DrawArea(new Point(1, 1), 28, 25);
            var renderer = new FakeRenderer(drawArea);
            var renderable = new RenderableEvent(_event, renderer, 1);

            renderable.Render(0);

            Assert.Equal(1, renderer.X);
            Assert.Equal(1, renderer.Y);
            Assert.Equal("1   19-01-01 00.00.00 GET   ", renderer.StringToRender);
            Assert.Equal(28, renderer.StringToRender.Length);
        }
    }

    public class A_RenderableEventHeader
    {
        [Fact]
        public void Should_render_header_in_the_correct_place()
        {
            var dimensions = new DrawArea(new Point(10, 5), 40, 25);
            var renderer = new FakeRenderer(dimensions);
            var renderable = new RenderableEventHeader(renderer);

            renderable.Render(0);

            Assert.Equal(10, renderer.X);
            Assert.Equal(5, renderer.Y);
            Assert.Equal("ID  DATETIME          OPER DATA         ", renderer.StringToRender);
            Assert.Equal(40, renderer.StringToRender.Length);
        }

        [Fact]
        public void Should_omit_data_header_if_not_enough_space()
        {
            var dimensions = new DrawArea(new Point(1, 1), 28, 25);
            var renderer = new FakeRenderer(dimensions);
            var renderable = new RenderableEventHeader(renderer);

            renderable.Render(0);

            Assert.Equal(1, renderer.X);
            Assert.Equal(1, renderer.Y);
            Assert.Equal("ID  DATETIME          OPER  ", renderer.StringToRender);
            Assert.Equal(28, renderer.StringToRender.Length);
        }
    }

    public class A_RenderableTitle
    {
        [Fact]
        public void Should_render_title_in_the_correct_place()
        {
            var dimensions = new DrawArea(new Point(10, 5), 40, 25);
            var renderer = new FakeRenderer(dimensions);
            var renderable = new RenderableEventTitle("A short title", renderer);

            renderable.Render(0);

            Assert.Equal(10, renderer.X);
            Assert.Equal(5, renderer.Y);
            Assert.Equal("=[A short title]========================", renderer.StringToRender);
            Assert.Equal(40, renderer.StringToRender.Length);
        }

        [Fact]
        public void Should_truncate_the_title_if_not_enough_space()
        {
            var dimensions = new DrawArea(new Point(1, 1), 28, 25);
            var renderer = new FakeRenderer(dimensions);
            var renderable = new RenderableEventTitle("A long title with too many letter to fit on the title bar", renderer);

            renderable.Render(0);

            Assert.Equal(1, renderer.X);
            Assert.Equal(1, renderer.Y);
            Assert.Equal("=[A long title with too ma]=", renderer.StringToRender);
            Assert.Equal(28, renderer.StringToRender.Length);
        }
    }
}