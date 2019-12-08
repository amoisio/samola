using System;
using System.Collections.Generic;
using System.Text;
using Samola.Collections;
using Xunit;

namespace Samole.Collections.Tests
{
    public class An_empty_discarding_circular_list
    {
        ICircularList<int> _buffer;

        public An_empty_discarding_circular_list()
        {
            _buffer = new DiscardingCircularList<int>(5);
        }

        [Fact]
        public void Has_its_size_during_constructions()
        {
            Assert.Equal(5, _buffer.Size);
        }

        [Fact]
        public void Contains_zero_items()
        {
            Assert.Equal(0, _buffer.Count);
        }

        [Fact]
        public void Can_be_enumerated_normally()
        {
            int c = 0;
            foreach(var item in _buffer)
            {
                c++;
            }

            Assert.Equal(0, c);
        }
    }

    public class An_initialized_discarding_circular_list
    {
        ICircularList<int> _buffer;

        public An_initialized_discarding_circular_list()
        {
            _buffer = new DiscardingCircularList<int>(4, new []{ 1, 2, 3, 4 });
        }

        [Fact]
        public void Has_its_size_during_constructions()
        {
            Assert.Equal(4, _buffer.Size);
        }

        [Fact]
        public void Contains_the_initial_items_when_initial_array_fits_into_buffer1()
        {
            Assert.Equal(4, _buffer.Count);
        }

        [Fact]
        public void Contains_the_initial_items_when_initial_array_fits_into_buffer2()
        {
            _buffer = new DiscardingCircularList<int>(4, new[] { 1, 2 });

            Assert.Equal(2, _buffer.Count);
        }

        [Fact]
        public void Throws_when_initial_array_does_not_fit_into_buffer()
        {
            Assert.Throws<ArgumentException>(() => new DiscardingCircularList<int>(4, new[] { 1, 2, 3, 5, 6 }));
        }

        [Fact]
        public void Can_be_enumerated_normally()
        {
            Assert.Collection(_buffer
                , i => Assert.Equal(1, i)
                , i => Assert.Equal(2, i)
                , i => Assert.Equal(3, i)
                , i => Assert.Equal(4, i));
        }
    }

    public class A_normal_discarding_circular_list
    {
        ICircularList<int> _buffer;

        public A_normal_discarding_circular_list()
        {
            _buffer = new DiscardingCircularList<int>(5, new[] { 1, 2, 3 }, true);
        }

        [Fact]
        public void Adds_items_to_its_end()
        {
            _buffer.Add(4);

            Assert.Equal(4, _buffer.Count);
            Assert.Collection(_buffer
                , i => Assert.Equal(1, i)
                , i => Assert.Equal(2, i)
                , i => Assert.Equal(3, i)
                , i => Assert.Equal(4, i));
        }

        [Fact]
        public void Removes_items_from_its_beginning()
        {
            _buffer.Remove();

            Assert.Equal(2, _buffer.Count);
            Assert.Collection(_buffer
                , i => Assert.Equal(2, i)
                , i => Assert.Equal(3, i));
        }

        [Fact]
        public void Discards_items_from_the_beginning()
        {
            _buffer.Add(4);
            _buffer.Add(5);
            _buffer.Add(6);

            Assert.Equal(5, _buffer.Count);
            Assert.Collection(_buffer
                , i => Assert.Equal(2, i)
                , i => Assert.Equal(3, i)
                , i => Assert.Equal(4, i)
                , i => Assert.Equal(5, i)
                , i => Assert.Equal(6, i));
        }

        [Fact]
        public void Clearing_removes_all_items()
        {
            _buffer.Clear();

            Assert.Equal(0, _buffer.Count);
        }

        [Fact]
        public void Runs_a_function_on_a_list_item_when_it_is_added()
        {
            StringBuilder sb = new StringBuilder();
            _buffer.OnIndexChange = item => sb.Append(item);

            _buffer.Add(6);

            Assert.Equal("6", sb.ToString());
        }

        [Fact]
        public void Runs_a_function_on_a_list_item_when_its_index_changes()
        {
            StringBuilder sb = new StringBuilder();
            _buffer = new DiscardingCircularList<int>(5, new[] { 1, 2, 3, 4, 5 }, true);
            _buffer.OnIndexChange = item => sb.Append(item);

            _buffer.Add(6);

            Assert.Equal("62345", sb.ToString());
        }
    }

    public class A_reversed_discarding_circular_list
    {
        ICircularList<int> _buffer;

        public A_reversed_discarding_circular_list()
        {
            _buffer = new DiscardingCircularList<int>(5, new[] { 1, 2, 3 }, false);
        }

        [Fact]
        public void Adds_items_to_its_beginning()
        {
            _buffer.Add(4);

            Assert.Equal(4, _buffer.Count);
            Assert.Collection(_buffer
                , i => Assert.Equal(4, i)
                , i => Assert.Equal(3, i)
                , i => Assert.Equal(2, i)
                , i => Assert.Equal(1, i));
        }

        [Fact]
        public void Removes_items_from_its_end()
        {
            _buffer.Remove();

            Assert.Equal(2, _buffer.Count);
            Assert.Collection(_buffer
                , i => Assert.Equal(3, i)
                , i => Assert.Equal(2, i));
        }

        [Fact]
        public void Discards_items_from_the_end()
        {
            _buffer.Add(4);
            _buffer.Add(5);
            _buffer.Add(6);

            Assert.Equal(5, _buffer.Count);
            Assert.Collection(_buffer
                , i => Assert.Equal(6, i)
                , i => Assert.Equal(5, i)
                , i => Assert.Equal(4, i)
                , i => Assert.Equal(3, i)
                , i => Assert.Equal(2, i));
        }

        [Fact]
        public void Clearing_removes_all_items()
        {
            _buffer.Clear();

            Assert.Equal(0, _buffer.Count);
        }

        [Fact]
        public void Runs_a_function_on_a_list_item_when_it_is_added()
        {
            StringBuilder sb = new StringBuilder();
            _buffer.OnIndexChange = item => sb.Append(item);

            _buffer.Add(6);

            Assert.Equal("1236", sb.ToString());
        }

        [Fact]
        public void Runs_a_function_on_a_list_item_when_its_index_changes()
        {
            StringBuilder sb = new StringBuilder();
            _buffer = new DiscardingCircularList<int>(5, new[] { 1, 2, 3, 4, 5 }, true);
            _buffer.OnIndexChange = item => sb.Append(item);

            _buffer.Add(6);

            Assert.Equal("62345", sb.ToString());
        }
    }
}
