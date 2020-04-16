using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GrocyDesktop.Management
{
	public class NginxServerManager
	{
		public NginxServerManager(string binDirectory, string arguments = "", Dictionary<string, string> environmentVariables = null)
		{
			this.BinDirectory = binDirectory;
			this.Arguments = arguments;
			this.EnvironmentVariables = environmentVariables;
			this.OutputLines = new List<string>();

			if (this.EnvironmentVariables == null)
			{
				this.EnvironmentVariables = new Dictionary<string, string>();
			}
		}

		private string BinDirectory;
		private string Arguments;
		private Dictionary<string, string> EnvironmentVariables;
		private Process Process;
		private List<string> OutputLines;
		
		public void Start()
		{
			this.Process = new Process();
			this.Process.StartInfo.UseShellExecute = false;
			this.Process.StartInfo.RedirectStandardOutput = true;
			this.Process.StartInfo.RedirectStandardError = true;
			this.Process.StartInfo.CreateNoWindow = true;
			this.Process.EnableRaisingEvents = true;
			this.Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			this.Process.OutputDataReceived += this.Process_OutputDataReceived;
			this.Process.ErrorDataReceived += this.Process_OutputDataReceived;

			this.Process.StartInfo.FileName = Path.Combine(this.BinDirectory, "nginx.exe");
			this.Process.StartInfo.Arguments = this.Arguments;
			this.Process.StartInfo.WorkingDirectory = this.BinDirectory;
			
			foreach (KeyValuePair<string, string> item in this.EnvironmentVariables)
			{
				this.Process.StartInfo.EnvironmentVariables.Add(item.Key, item.Value);
			}

			this.Process.Start();
			this.Process.BeginOutputReadLine();
			this.Process.BeginErrorReadLine();
		}

		public void Stop()
		{
			if (this.Process != null && !this.Process.HasExited)
			{
				Process nginxStopSignalProcess = new Process();
				nginxStopSignalProcess.StartInfo.UseShellExecute = false;
				nginxStopSignalProcess.StartInfo.CreateNoWindow = true;
				nginxStopSignalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				nginxStopSignalProcess.StartInfo.FileName = Path.Combine(this.BinDirectory, "nginx.exe");
				nginxStopSignalProcess.StartInfo.Arguments = "-s stop";
				nginxStopSignalProcess.StartInfo.WorkingDirectory = this.BinDirectory;
				nginxStopSignalProcess.Start();
				nginxStopSignalProcess.WaitForExit();

				if (!this.Process.HasExited)
				{
					try
					{
						this.Process.CloseMainWindow();
						this.Process.Kill();
					}
					catch (Exception)
					{ }
				}
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

		private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				this.OutputLines.Add(e.Data);
			}
		}
	}
}
