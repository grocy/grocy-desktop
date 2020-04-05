using System.Collections.Generic;

namespace GrocyDesktop
{
	public class BarcodeBuddyEnvironmentManager
	{
		public BarcodeBuddyEnvironmentManager(string basePath)
		{
			this.BasePath = basePath;
			this.EnvironmentVariables = new Dictionary<string, string>();
		}

		private string BasePath;
		private Dictionary<string, string> EnvironmentVariables;

		public void Setup(string grocyApiUrl)
		{
			this.SetSetting("GROCY_API_URL", grocyApiUrl);
			this.SetSetting("GROCY_API_KEY", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"); // Dummy, not needed, due to disabled authentication, but required
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
