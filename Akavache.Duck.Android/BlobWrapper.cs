using System;
using System.Linq;

using System.Reactive.Linq;

namespace Akavache.Duck
{
    public class BlobWrapper : IBlobWrapper
    {
        dynamic LocalMachine
        {
            get
            {
                return BlobCache.LocalMachine;
            }
        }

        public BlobWrapper()
        {
            BlobCache.ApplicationName = "BlobWrapper";
        }

        public dynamic InsertObject<T>(string key, T value)
        {
            return LocalMachine.InsertObject(key, value);
        }

        public dynamic GetObject<T>(string key)
        {
            return BlobCache.LocalMachine.GetObject<T>(key);
        }

        public dynamic InvalidateObject<T>(string key)
        {
            return LocalMachine.InvalidateObject<T>(key);
        }
    }
}