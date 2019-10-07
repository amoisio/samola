namespace Samola.Numbers.Cache
{
    internal class SingletonCacheLock
    {
        private static readonly SingletonCacheLock _instance = new SingletonCacheLock();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonCacheLock() { }

        private SingletonCacheLock() { }

        public static SingletonCacheLock Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
