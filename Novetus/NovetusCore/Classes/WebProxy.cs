#if LAUNCHER || URI
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;

public class WebProxy
{
    private static ProxyServer Server = new ProxyServer();
    private static ExplicitProxyEndPoint end;

    public bool HasStarted()
    {
        return Server.ProxyRunning;
    }

    public void Start()
    {
        try
        {
            //load ext
            Server.BeforeRequest += new AsyncEventHandler<SessionEventArgs>(OnRequest);
            UpdateEndPoint(true);
            Util.ConsolePrint("Web Proxy started on port " + GlobalVars.WebProxyPort, 3);
        }
        catch (Exception e)
        {
            Util.LogExceptions(e);
        }
    }

    public void UpdateEndPoint(bool shouldRunServer = false, bool decrypt = true)
    {
        if (Server.ProxyEndPoints.Count > 0)
        {
            Server.RemoveEndPoint(end);
        }

        GlobalVars.WebProxyPort = GlobalVars.UserConfiguration.RobloxPort + 1;
        end = new ExplicitProxyEndPoint(IPAddress.Any, GlobalVars.WebProxyPort, decrypt);
        end.BeforeTunnelConnectRequest += new AsyncEventHandler<TunnelConnectSessionEventArgs>(OnBeforeTunnelConnectRequest);
        Server.AddEndPoint(end);

        if (!Server.ProxyRunning && shouldRunServer)
        {
            Server.Start();
        }

        if (Server.ProxyRunning)
        {
            foreach (ProxyEndPoint endPoint in Server.ProxyEndPoints)
            {
                Server.SetAsSystemHttpProxy(end);
                Server.SetAsSystemHttpsProxy(end);
            }
        }

        Util.ConsolePrint("Web Proxy Endpoint updated with port " + GlobalVars.WebProxyPort, 3);
    }

    private bool IsURIAllowed(HttpWebClient client)
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

    private Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
    {
        if (!IsURIAllowed(e.HttpClient))
        {
            e.DecryptSsl = false;
        }

        return Task.CompletedTask;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task OnRequest(object sender, SessionEventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        if (!IsURIAllowed(e.HttpClient))
        {
            return;
        }

        e.GenericResponse("", HttpStatusCode.NotFound);
    }

    public void Stop()
    {
        Util.ConsolePrint("Web Proxy stopping on port " + GlobalVars.WebProxyPort, 3);
        Server.BeforeRequest -= new AsyncEventHandler<SessionEventArgs>(OnRequest);
        Server.Stop();
    }
}

public interface IWebProxyExtention
{

}
#endif
