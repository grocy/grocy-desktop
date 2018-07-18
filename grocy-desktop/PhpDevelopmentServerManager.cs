using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

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
		}

		private string BinDirectory;
		private string WwwDirectory;
		private Process PhpProcess;
		private List<string> OutputLines;

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
			this.PhpProcess = new Process();
			this.PhpProcess.StartInfo.UseShellExecute = false;
			this.PhpProcess.StartInfo.RedirectStandardOutput = true;
			this.PhpProcess.StartInfo.RedirectStandardError = true;
			this.PhpProcess.StartInfo.CreateNoWindow = true;
			this.PhpProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			this.PhpProcess.OutputDataReceived += this.PhpProcess_OutputDataReceived;
			this.PhpProcess.ErrorDataReceived += PhpProcess_OutputDataReceived;

			this.PhpProcess.StartInfo.FileName = Path.Combine(this.BinDirectory, "php.exe");
			this.PhpProcess.StartInfo.Arguments = "-S localhost:" + this.Port;
			this.PhpProcess.StartInfo.WorkingDirectory = this.WwwDirectory;

			this.PhpProcess.Start();
			this.PhpProcess.BeginOutputReadLine();
			this.PhpProcess.BeginErrorReadLine();
		}

		public void StopServer()
		{
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
