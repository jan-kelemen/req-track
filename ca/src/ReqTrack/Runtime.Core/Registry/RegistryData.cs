using System;
using System.Collections.Generic;

namespace ReqTrack.Runtime.Core.Registry
{
    public class RegistryData : IRegistry
    {
        private Dictionary<string, object> _factories = new Dictionary<string, object>();

        public T GetFactory<T>() where T : class
        {
            return _factories[nameof(T)] as T;
        }

        public bool RegisterFactory<T>(T factoryObject) where T : class
        {
            _factories[nameof(T)] = factoryObject;
            return true;
        }
    }
}
