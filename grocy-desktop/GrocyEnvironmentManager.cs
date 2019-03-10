using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GrocyDesktop
{
	public class GrocyEnvironmentManager
	{
		public GrocyEnvironmentManager(string grocyBasePath, string grocyDataPath)
		{
			this.BasePath = grocyBasePath;
			this.DataPath = grocyDataPath;
		}

		private string BasePath;
		private string DataPath;

		public void Setup(string baseUrl)
		{
			File.WriteAllText(Path.Combine(this.BasePath, "embedded.txt"), this.DataPath);
			this.SetSetting("CULTURE", this.GuessLocalization());
			this.SetSetting("BASE_URL", baseUrl);

			Extensions.CopyFolder(Path.Combine(this.BasePath, "data"), this.DataPath);
			File.Copy(Path.Combine(this.BasePath, "config-dist.php"), Path.Combine(this.DataPath, "config.php"), true);

			foreach (string item in Directory.GetFiles(Path.Combine(this.DataPath, "viewcache")))
			{
				File.Delete(item);
			}
		}

		public void SetSetting(string name, string value)
		{
			string settingOverridesFolderPath = Path.Combine(this.DataPath, "settingoverrides");
			if (!Directory.Exists(settingOverridesFolderPath))
			{
				Directory.CreateDirectory(settingOverridesFolderPath);
			}

			File.WriteAllText(Path.Combine(settingOverridesFolderPath, name + ".txt"), value);
		}

		private List<string> GetAvailableLocalizations()
		{
			List<string> list = new List<string>();
			foreach (string item in Directory.GetFiles(Path.Combine(this.BasePath, "localization")))
			{
				list.Add(Path.GetFileNameWithoutExtension(item));
			}
			return list;
		}

		private string GuessLocalization()
		{
			string systemCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower();
			if (this.GetAvailableLocalizations().Contains(systemCulture))
			{
				return systemCulture;
			}
			else
			{
				return "en";
			}
		}
	}
}
