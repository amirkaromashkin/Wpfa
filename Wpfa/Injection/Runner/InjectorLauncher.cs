using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using ManagedInjector;
using Wpfa.Injection.Server;

namespace Wpfa.Injection.Runner
{
    internal class InjectorLauncher : IInjectorLauncher
    {
        private IInsider _insider;

        private static string InsiderServiceUrl
        {
            get { return string.Format("ipc://{0}/{1}", WellKnownNames.ChannelName, WellKnownNames.InsiderObjectName); }
        }

        public bool Attach(Process process)
        {
            LaunchInjector(process);

            Thread.Sleep(500);
            
            _insider = GetInstance();

            return _insider.IsAlive();
        }

        public IInsider GetInstance()
        {
            return (IInsider)Activator.GetObject(typeof(Insider), InsiderServiceUrl);
        }

        private static void LaunchInjector(Process processName)
        {
            Type injectinType = typeof(InjectionBody);

            string codebase = injectinType.Assembly.CodeBase;

            codebase = codebase.Replace("file:///", string.Empty);

            Injector.Launch(
                processName.MainWindowHandle,
                codebase,
                injectinType.FullName,
                "SetupServer");
        }

        public IMessage InvokeInApplication(IMessage message)
        {
            return _insider.Invoke(message);
        }
    }
}