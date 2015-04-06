using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Wpfa.Injection.Runner
{
    internal class ApplicationRealProxy : RealProxy
    {
        private const int InjectionTriesCount = 10;

        private readonly string _exeFile;
        private readonly IInjectorLauncher _injector;
        private Process _process;

        public ApplicationRealProxy(Type fixtureType, string exeFile, IInjectorLauncher injector)
            : base(fixtureType)
        {
            _exeFile = exeFile;
            _injector = injector;
        }

        public override IMessage Invoke(IMessage msg)
        {
            var constructionCallMessage = msg as IConstructionCallMessage;
            if (constructionCallMessage != null)
            {
                Process process = CreateProcess();

                process.WaitForInputIdle();

                TryAttach(process, InjectionTriesCount);
            }

            return _injector.InvokeInApplication(msg);
        }

        private Process CreateProcess()
        {
            return _process = Process.Start(_exeFile);
        }

        private bool TryAttach(Process process, int times)
        {
            try
            {
                return _injector.Attach(process);
            }
            catch (Exception)
            {
                if (times > 0)
                {
                    return TryAttach(process, times - 1);
                }

                throw;
            }
        }

        ~ApplicationRealProxy()
        {
            if (_process != null && !_process.HasExited)
            {
                _process.Kill();
            }
        }
    }
}