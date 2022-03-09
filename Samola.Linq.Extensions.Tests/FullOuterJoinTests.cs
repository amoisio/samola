using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Samola.Linq.Extensions.Tests
{
    public class FullOuterJoinTests
    {
        [Fact]
        public void FullOuterJoin_returns_the_inner_list_if_outer_list_is_empty()
        {
            var inner = new TestEntity[]
            {
                new TestEntity { Key = 1},
                new TestEntity { Key = 2},
                new TestEntity { Key = 3},
                new TestEntity { Key = 4},
                new TestEntity { Key = 5},
                new TestEntity { Key = 6}
            };

            var outer = new TestEntity[]
            {

            };

            var joinArray = outer.FullOuterJoin(
                            inner,
                            o => o.Key,
                            i => i.Key,
                            (o, i) => Tuple.Create(o, i)
                            ).ToArray();

            Assert.Equal(inner, joinArray.Select(e => e.Item2));
        }

        [Fact]
        public void FullOuterJoin_retuns_the_outer_list_if_inner_list_is_empty()
        {
            var outer = new TestEntity[]
            {
                new TestEntity { Key = 1},
                new TestEntity { Key = 2},
                new TestEntity { Key = 3},
                new TestEntity { Key = 4},
                new TestEntity { Key = 5},
                new TestEntity { Key = 6}
            };

            var inner = new TestEntity[]
            {

            };

            var joinArray = outer.FullOuterJoin(
                            inner,
                            o => o.Key,
                            i => i.Key,
                            (o, i) => Tuple.Create(o, i)
                            ).ToArray();

            Assert.Equal(outer, joinArray.Select(e => e.Item1));
        }

        [Fact]
        public void FullOuterJoin_throws_argument_null_if_inner_array_is_null()
        {
            var outer = new TestEntity[]
            {
                new TestEntity { Key = 1},
            };

            TestEntity[] inner = null;

            try
            {
                var joins = outer.FullOuterJoin(
                            inner,
                            o => o.Key,
                            i => i.Key,
                            (o, i) => Tuple.Create(o, i)
                            ).ToArray();

                Assert.False(true);
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }

        [Fact]
        public void FullOuterJoin_pairs_matching_entries_and_nulls_with_non_matching_entries()
        {
            var outer = new TestEntity[]
            {
                new TestEntity { Key = 1, Opt = "outer1"},
                new TestEntity { Key = 2, Opt = "outer2"},
                new TestEntity { Key = 3, Opt = "outer3"},

            };

            var inner = new TestEntity[]
            {
                new TestEntity { Key = 2, Opt = "inner21"},
                new TestEntity { Key = 2, Opt = "inner22"},
                new TestEntity { Key = 3, Opt = "inner3"},
                new TestEntity { Key = 4, Opt = "inner4"},
            };

            var expected = new Tuple<string, string>[]
            {
                Tuple.Create<string, string>("outer1", null),
                Tuple.Create<string, string>("outer2", "inner21"),
                Tuple.Create<string, string>("outer2", "inner22"),
                Tuple.Create<string, string>("outer3", "inner3"),
                Tuple.Create<string, string>(null, "inner4"),
            };

            var result = outer.FullOuterJoin(inner,
                o => o.Key,
                i => i.Key,
                (o, i) => Tuple.Create(o?.Opt, i?.Opt)).ToArray();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void FullOuterJoin_can_use_string_as_key()
        {
            var outer = new TestEntity[]
            {
                new TestEntity { Key = 1, Opt = "outer1"},
                new TestEntity { Key = 2, Opt = "outer2"},
                new TestEntity { Key = 3, Opt = "outer3"},

            };

            var inner = new TestEntity[]
            {
                new TestEntity { Key = 2, Opt = "outer1"},
            };

            var expected = new Tuple<int, int>[]
            {
                Tuple.Create(1, 2),
                Tuple.Create(2, 0),
                Tuple.Create(3, 0)
            };

            var result = outer.FullOuterJoin(inner,
                o => o.Opt,
                i => i.Opt,
                (o, i) => Tuple.Create(o?.Key ?? 0, i?.Key ?? 0)).ToArray();

            Assert.Equal(expected, result);
        }
    }
}
