using System;
using System.Runtime.Remoting.Proxies;
using Wpfa.Injection.Runner;
using Injector = ManagedInjector.Injector;

namespace Wpfa.Injection
{
    public class ProcessIsolationAttribute : ProxyAttribute
    {
        private readonly string _fileName;

        public ProcessIsolationAttribute(string fileName)
        {
            _fileName = fileName;
        }

        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            return (MarshalByRefObject)new ApplicationRealProxy(serverType, _fileName, new InjectorLauncher()).GetTransparentProxy();
        }
    }
}