using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Caching;

namespace GrocyDesktop
{
	public class PhpProcessManager
	{
		public PhpProcessManager(string phpBinDirectoryPath, string workingDirectory, string arguments, Dictionary<string, string> environmentVariables = null)
		{
			this.BinDirectory = phpBinDirectoryPath;
			this.WorkingDirectory = workingDirectory;
			this.Arguments = arguments;
			this.OutputLines = new List<string>();
			this.PhpProcessUnintentedRestartsCache = new MemoryCache("PhpProcessManager_RestartCache" + Guid.NewGuid());

			if (this.EnvironmentVariables == null)
			{
				this.EnvironmentVariables = new Dictionary<string, string>();
			}
		}

		private string BinDirectory;
		private string WorkingDirectory;
		private string Arguments;
		private Dictionary<string, string> EnvironmentVariables;
		private Process PhpProcess;
		private List<string> OutputLines;
		private MemoryCache PhpProcessUnintentedRestartsCache;
		private bool NextPhpProcessExitIsIntended;

		private const int PHP_PROCESS_UNINTENDED_RESTARTS_TIMESPAN_MINUTES = 1;
		private const int PHP_PROCESS_MAX_UNINTENDED_RESTARTS = 5;
		public void Start()
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
			this.PhpProcess.StartInfo.Arguments = this.Arguments;
			this.PhpProcess.StartInfo.WorkingDirectory = this.WorkingDirectory;
			
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
					this.Start();
				}
			}
		}

		public void Stop()
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
	}
}
