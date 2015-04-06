using System;
using System.Runtime.Remoting.Proxies;
using Wpfa.Injection;
using Wpfa.Injection.Runner;

namespace Wpfa
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
            object createNormally = AppDomain.CurrentDomain.GetData(WellKnownNames.CreateInstanceNormallyMarker);
            if (createNormally is bool && (bool)createNormally)
            {
                return base.CreateInstance(serverType);
            }

            return (MarshalByRefObject)new ApplicationRealProxy(serverType, _fileName, new ApplicationInjector()).GetTransparentProxy();
        }
    }
}