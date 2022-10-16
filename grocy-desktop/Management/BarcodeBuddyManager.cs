using GrocyDesktop.Helpers;
using System.Collections.Generic;
using System.IO;

namespace GrocyDesktop.Management
{
	public class BarcodeBuddyManager
	{
		public BarcodeBuddyManager(string basePath, string dataPath, bool preferExternalAccess, int desiredPort = -1)
		{
			this.BasePath = basePath;
			this.DataPath = dataPath;
			this.PreferExternalAccess = preferExternalAccess;
			this.EnvironmentVariables = new Dictionary<string, string>();

			if (desiredPort == -1)
			{
				this.Port = NetHelper.GetRandomFreePort();
			}
			else
			{
				if (NetHelper.IsPortFree(desiredPort))
				{
					this.Port = desiredPort;
				}
				else
				{
					this.Port = NetHelper.GetRandomFreePort();
				}
			}
		}

		private string BasePath;
		private string DataPath;
		private bool PreferExternalAccess;
		private Dictionary<string, string> EnvironmentVariables;
		public int Port { get; private set; }

		public string IpUrl
		{
			get
			{
				return "http://" + NetHelper.GetNetworkIp() + ":" + this.Port.ToString();
			}
		}

		public string HostnameUrl
		{
			get
			{
				return "http://" + NetHelper.GetHostname() + ":" + this.Port.ToString();
			}
		}

		public string LocalUrl
		{
			get
			{
				return "http://localhost:" + this.Port.ToString();
			}
		}

		public string DesiredUrl
		{
			get
			{
				if (this.PreferExternalAccess)
				{
					return this.IpUrl;
				}
				else
				{
					return this.LocalUrl;
				}
			}
		}

		public void Setup(string grocyApiUrl)
		{
			if (!Directory.Exists(this.DataPath))
			{
				Directory.CreateDirectory(this.DataPath);
			}

			// Dummy API key, not needed, due to disabled authentication, but required
			this.SetSetting("OVERRIDDEN_USER_CONFIG", "GROCY_API_URL=" + grocyApiUrl + ";GROCY_API_KEY=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
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
