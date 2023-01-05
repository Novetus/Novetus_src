#if !BASICLAUNCHER
#region Usings
using Mono.Nat;
using System;
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
    }
    #endregion
}
#endif
