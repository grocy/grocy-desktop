using System.Collections.Generic;
using System.IO;

namespace GrocyDesktop
{
	public class BarcodeBuddyEnvironmentManager
	{
		public BarcodeBuddyEnvironmentManager(string basePath, string barcodeBuddyDataPath, int desiredPort = -1)
		{
			this.BasePath = basePath;
			this.DataPath = barcodeBuddyDataPath;
			this.EnvironmentVariables = new Dictionary<string, string>();

			if (desiredPort == -1)
			{
				this.Port = Extensions.GetRandomFreePort();
			}
			else
			{
				if (Extensions.IsPortFree(desiredPort))
				{
					this.Port = desiredPort;
				}
				else
				{
					this.Port = Extensions.GetRandomFreePort();
				}
			}
		}

		private string BasePath;
		private string DataPath;
		private Dictionary<string, string> EnvironmentVariables;
		public int Port { get; private set; }

		public string IpUrl
		{
			get
			{
				return "http://" + Extensions.GetNetworkIp() + ":" + this.Port.ToString();
			}
		}

		public string HostnameUrl
		{
			get
			{
				return "http://" + Extensions.GetHostname() + ":" + this.Port.ToString();
			}
		}

		public string LocalUrl
		{
			get
			{
				return "http://localhost:" + this.Port.ToString();
			}
		}

		public void Setup(string grocyApiUrl)
		{
			if (!Directory.Exists(this.DataPath))
			{
				Directory.CreateDirectory(this.DataPath);
			}

			// Dummy API key, not needed, due to disabled authentication, but required
			this.SetSetting("OVERRIDDEN_USER_CONFIG", "GROCY_API_URL="+grocyApiUrl+";GROCY_API_KEY=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
			this.SetSetting("DISABLE_AUTHENTICATION", "true");
			this.SetSetting("CONFIG_PATH", Path.Combine(this.DataPath, "config.php"));
			this.SetSetting("AUTHDB_PATH", Path.Combine(this.DataPath, "users.db"));
			this.SetSetting("DATABASE_PATH", Path.Combine(this.DataPath, "barcodebuddy.db"));
			this.SetSetting("CURL_ALLOW_INSECURE_SSL_CA", "true");
			this.SetSetting("HIDE_LINK_GROCY", "true");

			File.Copy(Path.Combine(this.BasePath, "config-dist.php"), Path.Combine(this.DataPath, "config.php"), true);
		}

		public void SetSetting(string name, string value)
		{
			string envVarKey = "BBUDDY_" + name.ToUpper();
			if (this.EnvironmentVariables.ContainsKey(envVarKey))
			{
				this.EnvironmentVariables[envVarKey] = value;
			}
			else
			{
				this.EnvironmentVariables.Add(envVarKey, value);
			}
		}

		public Dictionary<string, string> GetEnvironmentVariables()
		{
			return this.EnvironmentVariables;
		}
	}
}
