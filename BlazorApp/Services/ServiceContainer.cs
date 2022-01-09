using BlazorApp.Services;
using System;
using System.Collections;

namespace BlazorApp.Services
{
    public class ServiceContainer
    {
        Hashtable _Services = null;
        static ServiceContainer _Instance = null;

        public ServiceContainer()
        {
            _Services = new Hashtable();
        }

        internal void AddService(Type type, object instance)
        {
            if (_Services.ContainsKey(type))
                return;
            _Services.Add(type, instance);
        }

        internal void RemoveService(Type type)
        {
            if (!_Services.ContainsKey(type))
                return;
            _Services.Remove(type);
        }

        internal IService GetServiceInstance(Type type)
        {
            if (!_Services.ContainsKey(type))
                return null;
            return _Services[type] as IService;
        }

        public static ServiceContainer Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ServiceContainer();
                return _Instance;
            }
        }
    }
}
