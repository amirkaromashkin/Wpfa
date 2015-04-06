using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using Wpfa.Injection.Server;

namespace Wpfa.Injection.Runner
{
    internal interface IInjectorLauncher
    {
        bool Attach(Process process);
        IInsider GetInstance();
        IMessage InvokeInApplication(IMessage message);
    }
}