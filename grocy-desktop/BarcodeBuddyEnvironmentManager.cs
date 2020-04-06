using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GrocyDesktop
{
	public class BarcodeBuddyEnvironmentManager
	{
		public BarcodeBuddyEnvironmentManager(string basePath, string barcodeBuddyDataPath)
		{
			this.BasePath = basePath;
			this.DataPath = barcodeBuddyDataPath;
			this.Settings = new Dictionary<string, string>();
		}

		private string BasePath;
		private string DataPath;
		private Dictionary<string, string> Settings;

		public void Setup(string grocyApiUrl)
		{
			if (!Directory.Exists(this.DataPath))
			{
				Directory.CreateDirectory(this.DataPath);
			}

			this.SetSetting("GROCY_API_URL", grocyApiUrl);
			this.SetSetting("GROCY_API_KEY", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"); // Dummy, not needed, due to disabled authentication, but required
			this.SetSetting("DISABLE_AUTHENTICATION", "true");
			this.SetSetting("CONFIG_PATH", Path.Combine(this.DataPath, "config.php"));
			this.SetSetting("AUTHDB_PATH", Path.Combine(this.DataPath, "users.db"));
			this.SetSetting("DATABASE_PATH", Path.Combine(this.DataPath, "barcodebuddy.db"));

			File.Copy(Path.Combine(this.BasePath, "config-dist.php"), Path.Combine(this.DataPath, "config.php"), true);
		}

		public void SetSetting(string name, string value)
		{
			string settingKey = name.ToUpper();
			if (this.Settings.ContainsKey(settingKey))
			{
				this.Settings[settingKey] = value;
			}
			else
			{
				this.Settings.Add(settingKey, value);
			}
		}

		public Dictionary<string, string> GetEnvironmentVariables()
		{
			// Barcode Buddy needs the settings semicolon separated in a single environment var
			// Example: BBUDDY_OVERRIDDEN_USER_CONFIG="GROCY_API_URL=https://myurl/api/;GROCY_API_KEY=1234"

			StringBuilder allSettingsString = new StringBuilder();
			foreach (KeyValuePair<string, string> item in this.Settings)
			{
				allSettingsString.Append(item.Key);
				allSettingsString.Append("=");
				allSettingsString.Append(item.Value);
				allSettingsString.Append(";");
			}
			allSettingsString.Length--; // Removes the last character (trailing semicolon)
			
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("BBUDDY_OVERRIDDEN_USER_CONFIG", allSettingsString.ToString());
			return dict;
		}
	}
}
