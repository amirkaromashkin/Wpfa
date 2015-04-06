using System;
using System.Runtime.Remoting.Messaging;

namespace Wpfa.Injection.Server
{
    internal class Insider : MarshalByRefObject, IInsider
    {
        public IMessage Invoke(IMessage msg)
        {
            throw new NotImplementedException("From the inside!");
        }

        public bool IsAlive()
        {
            return true;
        }
    }
}