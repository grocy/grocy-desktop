using GrocyDesktop.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GrocyDesktop.Management
{
	public class GrocyManager
	{
		public GrocyManager(string basePath, string dataPath, int desiredPort = -1)
		{
			this.BasePath = basePath;
			this.DataPath = dataPath;

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

		public void Setup()
		{
			File.WriteAllText(Path.Combine(this.BasePath, "embedded.txt"), this.DataPath);
			this.SetSetting("CULTURE", this.GuessLocalization());
			this.SetSetting("BASE_URL", "/");
			this.SetSetting("CURRENCY", new RegionInfo(CultureInfo.CurrentCulture.LCID).ISOCurrencySymbol);

			IOHelper.CopyFolder(Path.Combine(this.BasePath, "data"), this.DataPath);
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
			foreach (string item in Directory.GetDirectories(Path.Combine(this.BasePath, "localization")))
			{
				list.Add(Path.GetFileName(item));
			}
			return list;
		}

		private string GuessLocalization()
		{
			List<string> availableLocalizations = this.GetAvailableLocalizations();
			string systemCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
			string systemSubculture = CultureInfo.CurrentCulture.Name.Replace("-", "_");
			
			// Try to find the "sub" culture first
			foreach (string item in availableLocalizations)
			{
				if (item.Equals(systemSubculture, StringComparison.OrdinalIgnoreCase))
				{
					return item;
				}
			}

			// Afterwards the "main" culture
			foreach (string item in availableLocalizations)
			{
				if (item.Equals(systemCulture, StringComparison.OrdinalIgnoreCase))
				{
					return item;
				}
			}

			return "en";
		}
	}
}
