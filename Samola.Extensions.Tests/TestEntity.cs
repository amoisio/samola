namespace Samola.Extensions.Tests
{
    public class TestEntity
    {
        public int Key { get; set; }
        public string Opt { get; set; }
        public string Value => $"{typeof(TestEntity).Name}_{Key}";
    }
}
