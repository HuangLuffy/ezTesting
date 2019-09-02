using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Win32;

namespace CommonLib.Util.net
{
    public class UtilIp
    {
        public static bool IsIp(string ip)
        {
            return IPAddress.TryParse(ip, out var ip1);
        }

        public static IList<string> GetPhysicsNetworkCardIp()
        {
            var networkCardIPs = new List<string>();
            var fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var adapter in fNetworkInterfaces)
            {
                var fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                var rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                if (rk == null) continue;
                // Distinguish PnpInstanceID   
                // Card that begins with PCI is physics.
                var pnpInstanceId = rk.GetValue("PnpInstanceID", "").ToString();
                var fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                if (pnpInstanceId.Length <= 3 || pnpInstanceId.Substring(0, 3) != "PCI") continue;
                var fIpInterfaceProperties = adapter.GetIPProperties();
                var unicastIpAddressInformationCollection = fIpInterfaceProperties.UnicastAddresses;
                foreach (var unicastIpAddressInformation in unicastIpAddressInformationCollection)
                {
                    if (unicastIpAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        networkCardIPs.Add(unicastIpAddressInformation.Address.ToString()); 
                    }
                }
            }
            return networkCardIPs;
        }
    }
}
