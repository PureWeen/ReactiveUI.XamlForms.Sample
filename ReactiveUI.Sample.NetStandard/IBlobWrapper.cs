using System;
using System.Reactive;

namespace Akavache.Duck
{
    public interface IBlobWrapper
    {
        dynamic GetObject<T>(string key);

        dynamic InsertObject<T>(string key, T value);
        dynamic InvalidateObject<T>(string key);
    }
}