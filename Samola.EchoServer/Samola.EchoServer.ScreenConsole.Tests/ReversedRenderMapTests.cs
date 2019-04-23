using EchoServer.ScreenConsole.Components;
using EchoServer.ScreenConsole.Renderer;
using EchoServer.ScreenConsole.Tests.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EchoServer.ScreenConsole.Tests
{
    public class ReversedRenderMapTests
    {
        ReversedRenderMap _map;
        IRenderer _renderer;

        public ReversedRenderMapTests()
        {
            _map = new ReversedRenderMap(5);
            _renderer = new FakeRenderer(new DrawArea(0, 0, 40, 20));
        }

        [Fact]
        public void An_empty_map_has_no_renderables()
        {
            Assert.Empty(_map.Renderables);
        }

        [Fact]
        public void Renderables_are_added_to_the_top_of_the_map()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            var r2 = new DummyRenderable("r2", _renderer);

            _map.Add(r1);
            _map.Add(r2);

            Assert.Equal(r1, _map.Renderables[1]);
            Assert.Equal(r2, _map.Renderables[0]);
        }

        [Fact]
        public void Map_can_contain_only_maxsize_rendables()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            var r2 = new DummyRenderable("r2", _renderer);
            var r3 = new DummyRenderable("r3", _renderer);
            var r4 = new DummyRenderable("r4", _renderer);
            var r5 = new DummyRenderable("r5", _renderer);
            var r6 = new DummyRenderable("r6", _renderer);

            _map.Add(r1);
            _map.Add(r2);
            _map.Add(r3);
            _map.Add(r4);
            _map.Add(r5);
            _map.Add(r6);

            Assert.Equal(5, _map.Renderables.Length);
        }

        [Fact]
        public void Old_renderables_are_removed_from_the_bottom()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            var r2 = new DummyRenderable("r2", _renderer);
            var r3 = new DummyRenderable("r3", _renderer);
            var r4 = new DummyRenderable("r4", _renderer);
            var r5 = new DummyRenderable("r5", _renderer);
            var r6 = new DummyRenderable("r6", _renderer);

            _map.Add(r1);
            _map.Add(r2);
            _map.Add(r3);
            _map.Add(r4);
            _map.Add(r5);

            Assert.Equal(r1, _map.Renderables[5]);

            _map.Add(r6);

            Assert.Equal(r2, _map.Renderables[5]);
            Assert.Equal(r6, _map.Renderables[0]);
        }

        [Fact]
        public void Renderables_are_marked_dirty_when_added_to_the_map()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            r1.Render(0);

            Assert.False(r1.IsDirty());

            _map.Add(r1);

            Assert.True(r1.IsDirty());
        }

        [Fact]
        public void Renderables_are_marked_dirty_when_moved_in_the_map()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            var r2 = new DummyRenderable("r2", _renderer);
            var r3 = new DummyRenderable("r3", _renderer);
            var r4 = new DummyRenderable("r4", _renderer);
            var r5 = new DummyRenderable("r5", _renderer);
            var r6 = new DummyRenderable("r6", _renderer);

            _map.Add(r1);
            _map.Add(r2);
            _map.Add(r3);
            _map.Add(r4);
            _map.Add(r5);

            foreach (var renderable in _map.Renderables)
            {
                renderable.Render(0);
            }

            Assert.False(_map.Renderables.All(r => r.IsDirty()));

            _map.Add(r6);

            Assert.True(_map.Renderables.All(r => r.IsDirty()));
        }

        [Fact]
        public void Map_has_changes_if_renderables_are_marked_dirty()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            _map.Add(r1);
            r1.Render(0);

            Assert.False(_map.HasChanges());

            r1.SetDirty();

            Assert.True(_map.HasChanges());
        }

        [Fact]
        public void Map_keeps_track_of_renderable_indeces()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            var r2 = new DummyRenderable("r2", _renderer);
            var r3 = new DummyRenderable("r3", _renderer);
            var r4 = new DummyRenderable("r4", _renderer);
            var r5 = new DummyRenderable("r5", _renderer);
            var r6 = new DummyRenderable("r6", _renderer);

            _map.Add(r1);
            _map.Add(r2);
            _map.Add(r3);
            _map.Add(r4);
            _map.Add(r5);

            Assert.Equal(2, _map.GetRenderMapIndex(r3));

            _map.Add(r6);

            Assert.Equal(3, _map.GetRenderMapIndex(r3));
        }

        [Fact]
        public void Getting_render_index_throws_for_not_mapped_renderables()
        {
            var r1 = new DummyRenderable("r1", _renderer);
            var r2 = new DummyRenderable("r2", _renderer);

            _map.Add(r1);

            Assert.Throws<KeyNotFoundException>(() => _map.GetRenderMapIndex(r2));
        }
    }
}
