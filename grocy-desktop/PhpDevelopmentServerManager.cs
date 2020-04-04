using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Caching;

namespace GrocyDesktop
{
	public class PhpDevelopmentServerManager
	{
		public PhpDevelopmentServerManager(string phpBinDirectoryPath, string wwwPath)
		{
			this.BinDirectory = phpBinDirectoryPath;
			this.WwwDirectory = wwwPath;
			this.Port = this.GetRandomFreePortNumber();
			this.OutputLines = new List<string>();
			this.PhpProcessUnintentedRestartsCache = new MemoryCache("PhpDevelopmentServerManager_RestartCache" + Guid.NewGuid());
		}

		private string BinDirectory;
		private string WwwDirectory;
		private Process PhpProcess;
		private List<string> OutputLines;
		private MemoryCache PhpProcessUnintentedRestartsCache;
		private bool NextPhpProcessExitIsIntended;

		private const int PHP_PROCESS_UNINTENDED_RESTARTS_TIMESPAN_MINUTES = 1;
		private const int PHP_PROCESS_MAX_UNINTENDED_RESTARTS = 5;

		public int Port { get; private set; }
		public string Url
		{
			get
			{
				return "http://localhost:" + this.Port.ToString();
			}
		}

		public void StartServer()
		{
			this.NextPhpProcessExitIsIntended = false;

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
			this.PhpProcess.StartInfo.Arguments = "-S localhost:" + this.Port;
			this.PhpProcess.StartInfo.WorkingDirectory = this.WwwDirectory;

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
			TcpListener l = new TcpListener(IPAddress.Loopback, 0);
			l.Start();
			int port = ((IPEndPoint)l.LocalEndpoint).Port;
			l.Stop();
			return port;
		}
	}
}
