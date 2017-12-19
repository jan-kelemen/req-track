using System;
using System.Collections.Generic;

namespace ReqTrack.Runtime.Core.Registry
{
    public class RegistryData : IRegistry
    {
        private readonly Dictionary<Type, object> _factories = new Dictionary<Type, object>();

        public T GetFactory<T>() where T : class => _factories[typeof(T)] as T;

        public bool RegisterFactory<T>(T factoryObject) where T : class
        {
            _factories[typeof(T)] = factoryObject;
            return true;
        }
    }
}
