/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
//using LiteNetLib;
using Mono.Nat;

public static class UPnP
{
	public static void InitUPnP(EventHandler<DeviceEventArgs> DeviceFound, EventHandler<DeviceEventArgs> DeviceLost)
	{
		if (GlobalVars.UPnP == true) {
			NatUtility.DeviceFound += DeviceFound;
			NatUtility.DeviceLost += DeviceLost;
			NatUtility.StartDiscovery();
		}
	}
		
	public static void StartUPnP(INatDevice device, Protocol protocol, int port)
	{
		if (GlobalVars.UPnP == true) {
			int map = device.GetSpecificMapping(protocol, port).PublicPort;
			
			if (map == -1) {
				device.CreatePortMap(new Mapping(protocol, port, port));
			}
		}
	}
		
	public static void StopUPnP(INatDevice device, Protocol protocol, int port)
	{
		if (GlobalVars.UPnP == true) {
			int map = device.GetSpecificMapping(protocol, port).PublicPort;
			
			if (map != -1) {
				device.DeletePortMap(new Mapping(protocol, port, port));
			}
		}
	}
}

/*
public static class UDP
{
	private static NetManager StartUDPListener(int port = -1)
	{
		if (GlobalVars.UDP == true)
		{
			EventBasedNetListener listener = new EventBasedNetListener();
			NetManager list = new NetManager(listener);
			if (port > -1)
			{
				list.Start(port);
			}
			else
			{
				list.Start();
			}

			return list;
		}

		return null;
	}

	public static NetManager StartClient(string ip, int port)
	{
		if (GlobalVars.UDP == true)
		{
			//we don't need a port here, we are a client.
			NetManager client = StartUDPListener();
			EventBasedNatPunchListener natPunchListener = new EventBasedNatPunchListener();
			client.Connect(ip, port, "");
			client.NatPunchEnabled = true;
			client.NatPunchModule.Init(natPunchListener);
			return client;
		}

		return null;
	}

	public static NetManager StartServer(int port)
	{
		if (GlobalVars.UDP == true)
		{
			NetManager server = StartUDPListener(port);
			EventBasedNatPunchListener natPunchListener = new EventBasedNatPunchListener();
			server.NatPunchEnabled = true;
			server.NatPunchModule.Init(natPunchListener);
			return server;
		}

		return null;
	}
}*/