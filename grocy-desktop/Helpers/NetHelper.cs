using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GrocyDesktop.Helpers
{
	public class NetHelper
	{
		private NetHelper()
		{ }

		public static int GetRandomFreePort()
		{
			TcpListener l = new TcpListener(IPAddress.Any, 0);
			l.Start();
			int port = ((IPEndPoint)l.LocalEndpoint).Port;
			l.Stop();
			return port;
		}

		public static bool IsPortFree(int port)
		{
			try
			{
				TcpListener l = new TcpListener(IPAddress.Any, port);
				l.Start();
				l.Stop();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static string GetHostname()
		{
			if (NetworkInterface.GetIsNetworkAvailable())
			{
				return Dns.GetHostName();
			}
			else
			{
				return "localhost";
			}
		}

		public static string GetNetworkIp()
		{
			if (NetworkInterface.GetIsNetworkAvailable())
			{
				var host = Dns.GetHostEntry(Dns.GetHostName());
				foreach (IPAddress item in host.AddressList)
				{
					if (item.AddressFamily == AddressFamily.InterNetwork)
					{
						return item.ToString();
					}
				}

				return "127.0.0.1";
			}
			else
			{
				return "127.0.0.1";
			}
		}
	}
}
