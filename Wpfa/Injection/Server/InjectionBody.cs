using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.ServiceModel;

namespace Wpfa.Injection.Server
{
    public static class InjectionBody
    {
        private static ServiceHost _serviceHost;
        private static IpcChannel _ipcChannel;

        public static void SetupServer()
        {
            SetupChannel(WellKnownNames.ChannelName);

            RemotingServices.Marshal(new Insider(), WellKnownNames.InsiderObjectName);
        }

        private static void SetupChannel(string channel)
        {
            if (_ipcChannel != null)
            {
                ChannelServices.UnregisterChannel(_ipcChannel);
            }

            var formatterSinkProvider = new BinaryServerFormatterSinkProvider()
            {
                TypeFilterLevel = TypeFilterLevel.Full
            };

            IDictionary props = new Hashtable();
            props["portName"] = channel;

            _ipcChannel = new IpcChannel(props, null, formatterSinkProvider);

            ChannelServices.RegisterChannel(_ipcChannel, false);
        }

        public static void Dispose()
        {
            _serviceHost.Close();
            _serviceHost = null;
        }
    }
}