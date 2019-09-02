using System;
using System.Collections.Specialized;
using Nancy;
using Nancy.Hosting.Self;
using RemoteLib.Listener;

namespace RemoteLib.Host.Nancy
{
    public class NancySelfHost : BaseListener
    {
        private NancyHost _host;
        private readonly Uri _baseUri;
        private readonly string _port = "9100";
        public NancySelfHost()
        {
            var listenerUrl = "http://localhost:" + _port;
            _baseUri = new Uri(listenerUrl);
        }
        private NancyHost CreateAndOpenSelfHost()
        {
            var hostConf = new HostConfiguration
            {
                RewriteLocalhost = true, UrlReservations = {CreateAutomatically = true}
            };
            return new NancyHost(
                new DefaultNancyBootstrapper(),
                hostConf,
                _baseUri);
        }
        public override void Stop()
        {
            try
            {
                _host?.Stop();
            }
            catch (Exception)
            {
                //logger.Error(ex, "The Listener server was not stopped.");
            }
            finally
            {
                _host = null;
            }
        }

        public override string GetAddress()
        {
            return GetIp() + ":" + GetPort();
        }

        public override string GetPort()
        {
            return _port;
        }

        public override void Start()
        {
            if (_host != null) return;
            _host = CreateAndOpenSelfHost();
            _host.Start();
        }
    }
}
