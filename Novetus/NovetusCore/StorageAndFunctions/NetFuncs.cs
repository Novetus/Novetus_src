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
        public static void InitUPnP(EventHandler<DeviceEventArgs> DeviceFound, EventHandler<DeviceEventArgs> DeviceLost)
        {
            if (GlobalVars.UserConfiguration.UPnP)
            {
                NatUtility.DeviceFound += DeviceFound;
                NatUtility.StartDiscovery();
            }
        }

        public static void StartUPnP(INatDevice device, Protocol protocol, int port)
        {
            if (GlobalVars.UserConfiguration.UPnP)
            {
                Mapping checker = device.GetSpecificMapping(protocol, port);
                int mapPublic = checker.PublicPort;
                int mapPrivate = checker.PrivatePort;

                if (mapPublic == -1 && mapPrivate == -1)
                {
                    Mapping portmap = new Mapping(protocol, port, port);
                    device.CreatePortMap(portmap);
                }
            }
        }

        public static void StopUPnP(INatDevice device, Protocol protocol, int port)
        {
            if (GlobalVars.UserConfiguration.UPnP)
            {
                Mapping checker = device.GetSpecificMapping(protocol, port);
                int mapPublic = checker.PublicPort;
                int mapPrivate = checker.PrivatePort;

                if (mapPublic != -1 && mapPrivate != -1)
                {
                    Mapping portmap = new Mapping(protocol, port, port);
                    device.DeletePortMap(portmap);
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
