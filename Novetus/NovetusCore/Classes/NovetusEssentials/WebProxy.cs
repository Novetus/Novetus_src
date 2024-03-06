#if LAUNCHER || URI
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;

namespace Novetus.Core
{
    public class IWebProxyExtension : IExtension
    {
        public virtual void OnProxyStart() { }
        public virtual void OnProxyStopped() { }

        public virtual bool IsValidURL(string absolutePath, string host) { return false; }

        public virtual Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e) { return Task.FromResult(0); }
        public virtual async Task OnRequest(object sender, SessionEventArgs e) 
        {
            string query = e.HttpClient.Request.RequestUri.Query;
            e.Ok("Response to '" + query + "'\nTest successful. \nRunning Novetus " + GlobalVars.ProgramInformation.Version + " on " + GlobalVars.ProgramInformation.NetVersion);
        }
    }

    public class WebProxy
    {
        private ProxyServer Server = new ProxyServer();
        private ExplicitProxyEndPoint end;
        public ExtensionManager Manager = new ExtensionManager();
        private static readonly SemaphoreLocker _locker = new SemaphoreLocker();
        public bool Started { get { return Server.ProxyRunning; } }
        private int WebProxyPort = 6171;

        public void DoSetup()
        {
            if (GlobalVars.UserConfiguration.ReadSettingBool("WebProxyInitialSetupRequired"))
            {
                string text = "Would you like to enable the Novetus web proxy?\n\n" +
                    "A web proxy redirects web traffic to a different location and in some cases can act as a gateway to different sites. Novetus uses the web proxy for additional client features and asset redirection.\n\n" +
                    "When enabling the web proxy, Novetus will locally create a certificate upon startup that ensures the proxy's functionality. Novetus will not send any user data to anyone, as everything involving the web proxy is entirely local to this computer.\n" +
                    "If you have any issue connecting to other web sites, including Roblox, closing Novetus or typing 'proxy off' into Novetus' console will fix it in most instances.\n\n" +
                    "Upon pressing 'Yes', Windows will ask you for permission to install the certificate.\n\n" +
                    "You can change this option at any time by typing 'proxy disable' or 'proxy on' in the Novetus console.\n\n" +
                    "NOTE: The Web proxy feature requires an Internet connection to function properly.\n\n" +
                    "This message will appear only once.\n";

                DialogResult result = MessageBox.Show(text, "Novetus - Web Proxy Opt-In", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                switch (result)
                {
                    case DialogResult.Yes:
                        GlobalVars.UserConfiguration.SaveSettingBool("WebProxyEnabled", true);
                        Start();
                        break;
                    case DialogResult.No:
                    default:
                        break;
                }

                GlobalVars.UserConfiguration.SaveSettingBool("WebProxyInitialSetupRequired", false);
            }
            else
            {
                if (GlobalVars.UserConfiguration.ReadSettingBool("WebProxyEnabled"))
                {
                    Start();
                }
            }
        }

        public void Start()
        {
            if (Server.ProxyRunning)
            {
                Util.ConsolePrint("The web proxy is already on and running.", 2);
                return;
            }

            try
            {
                Manager.LoadExtensions(GlobalPaths.NovetusExtsWebProxy);
                Util.ConsolePrint("Booting up Web Proxy...", 3);
                Server.CertificateManager.RootCertificateIssuerName = "Novetus";
                Server.CertificateManager.RootCertificateName = "Novetus Web Proxy";
                Server.BeforeRequest += new AsyncEventHandler<SessionEventArgs>(OnRequest);

                end = new ExplicitProxyEndPoint(IPAddress.Any, WebProxyPort, true);
                end.BeforeTunnelConnectRequest += new AsyncEventHandler<TunnelConnectSessionEventArgs>(OnBeforeTunnelConnectRequest);
                Server.AddEndPoint(end);

                Server.Start();

                foreach(ProxyEndPoint endPoint in Server.ProxyEndPoints)
                {
                    Server.SetAsSystemProxy(end, ProxyProtocolType.AllHttp);
                }

                Util.ConsolePrint("Web Proxy started on port " + WebProxyPort, 3);

                try
                {
                    foreach (IExtension extension in Manager.GetExtensionList().ToArray())
                    {
                        IWebProxyExtension webProxyExtension = extension as IWebProxyExtension;
                        if (webProxyExtension != null)
                        {
                            webProxyExtension.OnProxyStart();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception e)
            {
                Util.LogExceptions(e);
            }
        }

        private bool IsValidURL(HttpWebClient client)
        {
            string uri = client.Request.RequestUri.Host;

            if ((!uri.StartsWith("www.") &&
                !uri.StartsWith("web.") &&
                !uri.StartsWith("assetgame.") &&
                !uri.StartsWith("wiki.") &&
                !uri.EndsWith("api.roblox.com") &&
                !uri.StartsWith("roblox.com") || !uri.EndsWith("roblox.com")) &&
                !uri.EndsWith("robloxlabs.com"))
            {
                return false;
            }

            //we check the header
            HeaderCollection headers = client.Request.Headers;
            List<HttpHeader> userAgents = headers.GetHeaders("User-Agent");

            if (userAgents == null)
                return false;

            if (string.IsNullOrWhiteSpace(userAgents.FirstOrDefault().Value))
                return false;

            string ua = userAgents.FirstOrDefault().Value.ToLowerInvariant();

            //for some reason, this doesn't go through for the browser unless we look for mozilla/4.0.
            //this shouldn't break modern mozilla browsers though.
            return (ua.Contains("mozilla/4.0") || ua.Contains("roblox"));
        }

        private async Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            if (!IsValidURL(e.HttpClient))
            {
                e.DecryptSsl = false;
            }

            Uri uri = e.HttpClient.Request.RequestUri;

            foreach (IExtension extension in Manager.GetExtensionList().ToArray())
            {
                IWebProxyExtension webProxyExtension = extension as IWebProxyExtension;
                if (webProxyExtension != null)
                {
                    if (webProxyExtension.IsValidURL(uri.AbsolutePath.ToLowerInvariant(), uri.Host))
                    {
                        try
                        {
                            await webProxyExtension.OnBeforeTunnelConnectRequest(sender, e);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private async Task OnRequest(object sender, SessionEventArgs e)
        {
            await _locker.LockAsync(async () =>
            {
                if (!IsValidURL(e.HttpClient))
                {
                    return;
                }

                Uri uri = e.HttpClient.Request.RequestUri;

                foreach (IExtension extension in Manager.GetExtensionList().ToArray())
                {
                    IWebProxyExtension webProxyExtension = extension as IWebProxyExtension;
                    if (webProxyExtension != null)
                    {
                        if (webProxyExtension.IsValidURL(uri.AbsolutePath.ToLowerInvariant(), uri.Host))
                        {
                            try
                            {
                                await webProxyExtension.OnRequest(sender, e);
                                return;
                            }
                            catch (Exception)
                            {
                                e.GenericResponse("", HttpStatusCode.InternalServerError);
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                e.GenericResponse("", HttpStatusCode.NotFound);
            });
        }

        public void Stop()
        {
            try
            {
                if (!Server.ProxyRunning)
                {
                    Util.ConsolePrint("The web proxy is already turned off.", 2);
                    return;
                }

                Util.ConsolePrint("Web Proxy stopping on port " + WebProxyPort, 3);
                Server.BeforeRequest -= new AsyncEventHandler<SessionEventArgs>(OnRequest);
                Server.Stop();

                foreach (IExtension extension in Manager.GetExtensionList().ToArray())
                {
                    try
                    {
                        IWebProxyExtension webProxyExtension = extension as IWebProxyExtension;
                        if (webProxyExtension != null)
                        {
                            webProxyExtension.OnProxyStopped();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                Manager.UnloadExtensions();
                Manager.GetExtensionList().Clear();
            }
            catch
            {
            }
        }
    }
}
#endif
