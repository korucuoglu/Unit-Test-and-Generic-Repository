namespace Nortwind.Cache
{
    public abstract class CacheProvider
    {
        public static int CacheDuration = 60;
        public static CacheProvider Instance; // Cache Provider'dan kalıtım verdiğim sınıftan nesne türettiğimde statik olarak bunu da tutacağım.
        public abstract void Set(string key, object value); // abstract olduğu için prototip tanımlayabiliyoruz.
        public abstract object Get(string key);
        public abstract void Remove(string key);

        public abstract bool IsExist(string key);

    }
}
