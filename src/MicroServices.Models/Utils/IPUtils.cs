using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MicroServices.Models
{
    public static class IPUtils
    {
        public static string GetLocalIp()
        {
            return Dns.GetHostAddresses(Dns.GetHostName())
                .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                .FirstOrDefault()
                ?.ToString();
        }
    }
}
