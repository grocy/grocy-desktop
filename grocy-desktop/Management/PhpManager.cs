using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GrocyDesktop.Management
{
	public class PhpManager
	{
		public PhpManager(string binDirectory, string workingDirectory, string arguments, bool useFastCgi, Dictionary<string, string> environmentVariables = null)
		{
			this.BinDirectory = binDirectory;
			this.WorkingDirectory = workingDirectory;
			this.Arguments = arguments;
			this.EnvironmentVariables = environmentVariables;
			this.UseFastCgi = useFastCgi;
			this.OutputLines = new List<string>();

			if (this.EnvironmentVariables == null)
			{
				this.EnvironmentVariables = new Dictionary<string, string>();
			}
		}

		private string BinDirectory;
		private string WorkingDirectory;
		private string Arguments;
		private Dictionary<string, string> EnvironmentVariables;
		private Process Process;
		private List<string> OutputLines;
		private bool UseFastCgi;
		
		public void Start()
		{
			this.Process = new Process();
			this.Process.StartInfo.UseShellExecute = false;
			this.Process.StartInfo.RedirectStandardOutput = true;
			this.Process.StartInfo.RedirectStandardError = true;
			this.Process.StartInfo.RedirectStandardInput = true;
			this.Process.StartInfo.CreateNoWindow = true;
			this.Process.EnableRaisingEvents = true;
			this.Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			this.Process.OutputDataReceived += this.Process_OutputDataReceived;
			this.Process.ErrorDataReceived += this.Process_OutputDataReceived;

			string exe = "php.exe";
			if (this.UseFastCgi)
			{
				exe = "php-cgi.exe";
			}

			this.Process.StartInfo.FileName = Path.Combine(this.BinDirectory, exe);
			this.Process.StartInfo.Arguments = this.Arguments;
			this.Process.StartInfo.WorkingDirectory = this.WorkingDirectory;
			
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
				try
				{
					this.Process.CloseMainWindow();
					this.Process.Kill();
				}
				catch (Exception)
				{ }
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
