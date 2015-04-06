using System.Runtime.Remoting.Messaging;
namespace Wpfa.Injection.Server
{
    internal interface IInsider
    {
        IMessage Invoke(IMessage msg);

        bool IsAlive();
    }
}