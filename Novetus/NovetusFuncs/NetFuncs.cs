#region Usings
using Mono.Nat;
using System;
#endregion

#region NetFuncs

public static class NetFuncs
{
    public static void InitUPnP(EventHandler<DeviceEventArgs> DeviceFound, EventHandler<DeviceEventArgs> DeviceLost)
    {
        if (GlobalVars.UserConfiguration.UPnP == true)
        {
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.DeviceLost += DeviceLost;
            NatUtility.StartDiscovery();
        }
    }

    public static void StartUPnP(INatDevice device, Protocol protocol, int port)
    {
        if (GlobalVars.UserConfiguration.UPnP == true)
        {
            Mapping checker = device.GetSpecificMapping(protocol, port);
            int mapPublic = checker.PublicPort;
            int mapPrivate = checker.PrivatePort;

            if (mapPublic == -1 && mapPrivate == -1)
            {
                Mapping portmap = new Mapping(protocol, port, port);
                portmap.Description = "Novetus";
                device.CreatePortMap(portmap);
            }
        }
    }

    public static void StopUPnP(INatDevice device, Protocol protocol, int port)
    {
        if (GlobalVars.UserConfiguration.UPnP == true)
        {
            Mapping checker = device.GetSpecificMapping(protocol, port);
            int mapPublic = checker.PublicPort;
            int mapPrivate = checker.PrivatePort;

            if (mapPublic != -1 && mapPrivate != -1)
            {
                Mapping portmap = new Mapping(protocol, port, port);
                portmap.Description = "Novetus";
                device.DeletePortMap(portmap);
            }
        }
    }
}
#endregion
