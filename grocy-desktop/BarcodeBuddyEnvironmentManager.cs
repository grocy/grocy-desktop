using System.Collections.Generic;
using System.IO;

namespace GrocyDesktop
{
	public class BarcodeBuddyEnvironmentManager
	{
		public BarcodeBuddyEnvironmentManager(string basePath, string barcodeBuddyDataPath)
		{
			this.BasePath = basePath;
			this.DataPath = barcodeBuddyDataPath;
			this.EnvironmentVariables = new Dictionary<string, string>();
		}

		private string BasePath;
		private string DataPath;
		private Dictionary<string, string> EnvironmentVariables;

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
