/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using System.Linq;
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