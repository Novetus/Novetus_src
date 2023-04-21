#if !BASICLAUNCHER
#region Usings
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.Web;
using Titanium.Web.Proxy.Models;
#endregion

namespace Novetus.Core
{
    #region NetFuncs
    public static class NetFuncs
    {
        public static void InitUPnP()
        {
            if (GlobalVars.UserConfiguration.ReadSettingBool("UPnP"))
            {
                try
                {
                    NatUtility.DeviceFound += DeviceFound;
                    NatUtility.StartDiscovery();
                    Util.ConsolePrint("UPnP: Service initialized", 3);
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    Util.ConsolePrint("UPnP: Unable to initialize UPnP. Reason - " + ex.Message, 2);
                }
            }
        }

        public static void DeviceFound(object sender, DeviceEventArgs args)
        {
            try
            {
                INatDevice device = args.Device;
                string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP")) ? GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP") : device.GetExternalIP().ToString();
                Util.ConsolePrint("UPnP: Device '" + IP + "' registered.", 3);
                StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.ReadSettingInt("RobloxPort"));
                StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.ReadSettingInt("RobloxPort"));
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                Util.ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2);
            }
        }

        public static void StartUPnP(INatDevice device, Protocol protocol, int port)
        {
            if (GlobalVars.UserConfiguration.ReadSettingBool("UPnP"))
            {
                try
                {
                    Mapping checker = device.GetSpecificMapping(protocol, port);
                    int mapPublic = checker.PublicPort;
                    int mapPrivate = checker.PrivatePort;

                    if (mapPublic == -1 && mapPrivate == -1)
                    {
                        Mapping portmap = new Mapping(protocol, port, port);
                        device.CreatePortMap(portmap);
                    }

                    string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP")) ? GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP") : device.GetExternalIP().ToString();
                    Util.ConsolePrint("UPnP: Port " + port + " opened on '" + IP + "' (" + protocol.ToString() + ")", 3);
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    Util.ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2);
                }
            }
        }

        public static void StopUPnP(INatDevice device, Protocol protocol, int port)
        {
            if (GlobalVars.UserConfiguration.ReadSettingBool("UPnP"))
            {
                try
                {
                    Mapping checker = device.GetSpecificMapping(protocol, port);
                    int mapPublic = checker.PublicPort;
                    int mapPrivate = checker.PrivatePort;

                    if (mapPublic != -1 && mapPrivate != -1)
                    {
                        Mapping portmap = new Mapping(protocol, port, port);
                        device.DeletePortMap(portmap);
                    }

                    string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP")) ? GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP") : device.GetExternalIP().ToString();
                    Util.ConsolePrint("UPnP: Port " + port + " closed on '" + IP + "' (" + protocol.ToString() + ")", 3);
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    Util.ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2);
                }
            }
        }

        public static string FindQueryString(Uri uri, string searchQuery)
        {
            return FindQueryString(uri.Query, searchQuery);
        }

        public static string FindQueryString(string query, string searchQuery)
        {
            return HttpUtility.ParseQueryString(query)[searchQuery];
        }

        public static IEnumerable<HttpHeader> GenerateHeaders(string content, string contenttype = "")
        {
            List<HttpHeader> HeaderList = new List<HttpHeader>();

            if (!string.IsNullOrWhiteSpace(contenttype))
            {
                HeaderList.Add(new HttpHeader("Content-Type", contenttype));
            }

            HeaderList.Add(new HttpHeader("Content-Length", content));
            HeaderList.Add(new HttpHeader("Cache-Control", "no-cache"));

            return HeaderList;
        }
    }
    #endregion
}
#endif
