using System.Collections.Generic;

namespace ZeusCore
{
    public class Locator
    {
        private static readonly Dictionary<System.Type, object> _services = new ();
        public static bool Initialized { get; private set; } = false;


        public Locator()
        {
            Initialized = true;
        }


        public static void AddService<T>(T service)
        {
            var key = typeof(T);

            if (!_services.ContainsKey(key)) _services.Add(key, service);

            else throw ServiceAlreadyExistsMessage(key);
        }


        public static T GetService<T>()
        {
            var key = typeof(T);

            if (_services.ContainsKey(key)) return (T)_services[key];

            else throw ServiceNotFoundMessage(key);
        }


        public static void RemoveService<T>(T service)
        {
            var key = service.GetType();

            if (_services.ContainsKey(key)) _services.Remove(key);

            else throw ServiceNotFoundMessage(key);
        }


        public static bool ServiceExists<T>()
        {
            var key = typeof(T);

            return _services.ContainsKey(key);
        }


        #region Exceptions functions
        private static System.InvalidOperationException ServiceNotFoundMessage(System.Type type)
        {
            return new System.InvalidOperationException($"Service {type} not found.");
        }


        private static System.InvalidOperationException ServiceAlreadyExistsMessage(System.Type type)
        {
            return new System.InvalidOperationException($"Service {type} already exists.");
        }
        #endregion


    }
}

