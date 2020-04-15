using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public static class Extensions
	{
		public static void CopyFolder(string source, string destination)
		{
			foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
			{
				Directory.CreateDirectory(dirPath.Replace(source, destination));
			}

			foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(newPath, newPath.Replace(source, destination), true);
			}
		}

		public static void RestartApp()
		{
			Application.Restart();
			Environment.Exit(0);
		}

		public static void ExtractZipToDirectory(string archiveFilePath, string destinationDirectory, bool overwrite)
		{
			using (ZipArchive archive = new ZipArchive(File.OpenRead(archiveFilePath)))
			{
				if (!overwrite)
				{
					archive.ExtractToDirectory(destinationDirectory);
					return;
				}

				foreach (ZipArchiveEntry file in archive.Entries)
				{
					string completeFileName = Path.Combine(destinationDirectory, file.FullName);
					string directory = Path.GetDirectoryName(completeFileName);

					if (!Directory.Exists(directory))
					{
						Directory.CreateDirectory(directory);
					}


					if (file.Name != string.Empty)
					{
						file.ExtractToFile(completeFileName, true);
					}
				}
			}
		}

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

		public static void ReplaceInTextFile(string filePath, string placeholder, string value)
		{
			string text = File.ReadAllText(filePath);
			text = text.Replace(placeholder, value);
			File.WriteAllText(filePath, text);
		}
	}
}
