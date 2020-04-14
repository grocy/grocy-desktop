using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Caching;

namespace GrocyDesktop
{
	public class PhpDevelopmentServerManager
	{
		public PhpDevelopmentServerManager(string phpBinDirectoryPath, string wwwPath, bool externallyAccessible, Dictionary<string, string> environmentVariables = null, int desiredPort = -1)
		{
			this.BinDirectory = phpBinDirectoryPath;
			this.WwwDirectory = wwwPath;
			this.ExternallyAccessible = externallyAccessible;
			this.EnvironmentVariables = environmentVariables;
			this.OutputLines = new List<string>();
			this.PhpProcessUnintentedRestartsCache = new MemoryCache("PhpDevelopmentServerManager_RestartCache" + Guid.NewGuid());

			if (desiredPort == -1)
			{
				this.Port = this.GetRandomFreePortNumber();
			}
			else
			{
				if (this.IsPortFree(desiredPort))
				{
					this.Port = desiredPort;
				}
				else
				{
					this.Port = this.GetRandomFreePortNumber();
				}
			}

			if (this.EnvironmentVariables == null)
			{
				this.EnvironmentVariables = new Dictionary<string, string>();
			}
		}

		private string BinDirectory;
		private string WwwDirectory;
		private bool ExternallyAccessible;
		private Dictionary<string, string> EnvironmentVariables;
		private Process PhpProcess;
		private List<string> OutputLines;
		private MemoryCache PhpProcessUnintentedRestartsCache;
		private bool NextPhpProcessExitIsIntended;

		private const int PHP_PROCESS_UNINTENDED_RESTARTS_TIMESPAN_MINUTES = 1;
		private const int PHP_PROCESS_MAX_UNINTENDED_RESTARTS = 5;

		public int Port { get; private set; }
		public string IpUrl
		{
			get
			{
				return "http://" + this.GetNetworkIp() + ":" + this.Port.ToString();
			}
		}

		public string HostnameUrl
		{
			get
			{
				return "http://" + this.GetHostname() + ":" + this.Port.ToString();
			}
		}

		public string LocalUrl
		{
			get
			{
				return "http://localhost:" + this.Port.ToString();
			}
		}

		public void StartServer()
		{
			this.NextPhpProcessExitIsIntended = false;

			string bindingEndpoint = "localhost";
			if (this.ExternallyAccessible)
			{
				bindingEndpoint = "0.0.0.0";
			}

			this.PhpProcess = new Process();
			this.PhpProcess.StartInfo.UseShellExecute = false;
			this.PhpProcess.StartInfo.RedirectStandardOutput = true;
			this.PhpProcess.StartInfo.RedirectStandardError = true;
			this.PhpProcess.StartInfo.CreateNoWindow = true;
			this.PhpProcess.EnableRaisingEvents = true;
			this.PhpProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			this.PhpProcess.OutputDataReceived += this.PhpProcess_OutputDataReceived;
			this.PhpProcess.ErrorDataReceived += PhpProcess_OutputDataReceived;
			this.PhpProcess.Exited += PhpProcess_Exited;

			this.PhpProcess.StartInfo.FileName = Path.Combine(this.BinDirectory, "php.exe");
			this.PhpProcess.StartInfo.Arguments = "-S " + bindingEndpoint + ":" + this.Port;
			this.PhpProcess.StartInfo.WorkingDirectory = this.WwwDirectory;
			
			foreach (KeyValuePair<string, string> item in this.EnvironmentVariables)
			{
				this.PhpProcess.StartInfo.EnvironmentVariables.Add(item.Key, item.Value);
			}

			this.PhpProcess.Start();
			this.PhpProcess.BeginOutputReadLine();
			this.PhpProcess.BeginErrorReadLine();
		}

		private void PhpProcess_Exited(object sender, EventArgs e)
		{
			// When the process exits without intention, restart it for PHP_PROCESS_MAX_UNINTENDED_RESTARTS times
			// but only when this not happens more often than PHP_PROCESS_MAX_UNINTENDED_RESTARTS in PHP_PROCESS_UNINTENDED_RESTARTS_TIMESPAN_MINUTES minutes
			
			if (!this.NextPhpProcessExitIsIntended)
			{
				this.PhpProcessUnintentedRestartsCache.Add(new CacheItem(DateTime.Now.ToString(), "dummy"), new CacheItemPolicy() { AbsoluteExpiration = DateTime.UtcNow.AddMinutes(PHP_PROCESS_UNINTENDED_RESTARTS_TIMESPAN_MINUTES) });

				if (this.PhpProcessUnintentedRestartsCache.GetCount() <= PHP_PROCESS_MAX_UNINTENDED_RESTARTS)
				{
					this.StartServer();
				}
			}
		}

		public void StopServer()
		{
			this.NextPhpProcessExitIsIntended = true;

			if (this.PhpProcess != null && !this.PhpProcess.HasExited)
			{
				this.PhpProcess.Kill();
			}
		}

		public void SetEnvironmenVariables(Dictionary<string, string> environmentVariables)
		{
			this.EnvironmentVariables = environmentVariables;
		}

		public string GetConsoleOutput()
		{
			return string.Join(Environment.NewLine, this.OutputLines);
		}

		private void PhpProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				this.OutputLines.Add(e.Data);
			}
		}

		private int GetRandomFreePortNumber()
		{
			TcpListener l = new TcpListener(IPAddress.Any, 0);
			l.Start();
			int port = ((IPEndPoint)l.LocalEndpoint).Port;
			l.Stop();
			return port;
		}

		private bool IsPortFree(int port)
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

		private string GetHostname()
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

		private string GetNetworkIp()
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
