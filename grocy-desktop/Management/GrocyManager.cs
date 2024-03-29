using GrocyDesktop.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GrocyDesktop.Management
{
	public class GrocyManager
	{
		public GrocyManager(string basePath, string dataPath, bool preferExternalAccess, int desiredPort = -1)
		{
			this.BasePath = basePath;
			this.DataPath = dataPath;
			this.PreferExternalAccess = preferExternalAccess;

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

		public void Setup()
		{
			File.WriteAllText(Path.Combine(this.BasePath, "embedded.txt"), this.DataPath);
			this.SetSetting("DEFAULT_LOCALE", this.GuessLocalization());
			this.SetSetting("BASE_URL", "/");
			this.SetSetting("CURRENCY", new RegionInfo(CultureInfo.CurrentCulture.LCID).ISOCurrencySymbol);

			IOHelper.CopyFolder(Path.Combine(this.BasePath, "data"), this.DataPath);
			File.Copy(Path.Combine(this.BasePath, "config-dist.php"), Path.Combine(this.DataPath, "config.php"), true);
			File.Copy(Path.Combine(this.BasePath, "config-dist.php"), Path.Combine(this.BasePath, "data", "config.php"), true); // Dummy

			// Set the PHP timezone to the system ones by adding code calling date_default_timezone_set to (the end of) config.php
			int utcOffsetHours = DateTimeOffset.Now.Offset.Hours;
			string isDst = "0";
			if (TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now))
			{
				isDst = "1";
			}
			File.AppendAllText(Path.Combine(this.DataPath, "config.php"), $"{System.Environment.NewLine}date_default_timezone_set(timezone_name_from_abbr(\"\", {utcOffsetHours}*3600, {isDst}));");

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
