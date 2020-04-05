using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public class GrocyDesktopDependencyManager
	{
		private const string LATEST_GROCY_RELEASE_URL = "https://releases.grocy.info/latest";

		private static string LATEST_BARCODE_BUDDY_RELEASE_URL
		{
			get
			{
				// master branch for now (testing)
				return "https://github.com/Forceu/barcodebuddy/archive/master.zip";

				//using (WebClient wc = new WebClient())
				//{
				//	wc.Headers.Add("User-Agent", "grocy-desktop/" + Program.RunningVersion);
				//	string latestReleaseJson = wc.DownloadString("https://api.github.com/repos/Forceu/barcodebuddy/releases/latest");
				//	JObject latestRelease = JObject.Parse(latestReleaseJson);
				//	return "https://github.com/Forceu/barcodebuddy/archive/" + latestRelease["tag_name"] + ".zip";
				//}
			}
		}

		public readonly static string CefExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "cef");
		public readonly static string CefCachePath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "cef-cache");
		public readonly static string PhpExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "php");
		public readonly static string GrocyExecutingPath = Path.Combine(Program.BaseFixedUserDataFolderPath, "grocy");
		public readonly static string BarcodeBuddyExecutingPath = Path.Combine(Program.BaseFixedUserDataFolderPath, "barcodebuddy");

		public static async Task UnpackIncludedDependenciesIfNeeded(UserSettings settings, Form ownerFormReference = null)
		{
			FrmWait waitWindow = null;
			if (ownerFormReference != null)
			{
				waitWindow = new FrmWait();
				waitWindow.Show(ownerFormReference);
			}

			string vc2019x86ZipPath = Path.Combine(Program.BaseExecutingPath, "vc2019x86.zip");

			// CefSharp x86
			string cefZipPathx86 = Path.Combine(Program.BaseExecutingPath, "cefx86.zip");
			string cefPathx86 = Path.Combine(CefExecutingPath, "x86");
			if (!Directory.Exists(cefPathx86))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing embedded web browser (x86)...");
				}
				await Task.Run(() => Extensions.ExtractZipToDirectory(cefZipPathx86, cefPathx86, true));

				// Seems that CEF 75 has now bundled the VC runtime dependencies itself...
				//await Task.Run(() => ZipFile.ExtractToDirectory(vc2019x86ZipPath, cefPathx86));
			}

			// PHP
			string phpZipPath = Path.Combine(Program.BaseExecutingPath, "php.zip");
			if (!Directory.Exists(PhpExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing embedded PHP server...");
				}
				await Task.Run(() => Extensions.ExtractZipToDirectory(phpZipPath, PhpExecutingPath, true));
				await Task.Run(() => Extensions.ExtractZipToDirectory(vc2019x86ZipPath, PhpExecutingPath, true));
			}

			// grocy
			string grocyZipPath = Path.Combine(Program.BaseExecutingPath, "grocy.zip");
			if (!Directory.Exists(GrocyExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing grocy...");
				}
				await Task.Run(() => Extensions.ExtractZipToDirectory(grocyZipPath, GrocyExecutingPath, true));
			}

			// Barcode Buddy
			if (settings.EnableBarcodeBuddyIntegration)
			{
				string barcodeBuddyZipPath = Path.Combine(Program.BaseExecutingPath, "barcodebuddy.zip");
				if (!Directory.Exists(BarcodeBuddyExecutingPath))
				{
					if (waitWindow != null)
					{
						waitWindow.SetStatus("Preparing Barcode Buddy...");
					}
					await Task.Run(() => Extensions.ExtractZipToDirectory(barcodeBuddyZipPath, BarcodeBuddyExecutingPath + "-tmp", true));
					Directory.Move(Directory.GetDirectories(BarcodeBuddyExecutingPath + "-tmp").First(), BarcodeBuddyExecutingPath);
					Directory.Delete(BarcodeBuddyExecutingPath + "-tmp", true);
				}
			}

			// Cleanup old runtime dependency folders
			foreach (string item in Directory.GetDirectories(Program.RuntimeDependenciesBasePath))
			{
				if (new DirectoryInfo(item).Name != Program.RunningVersion)
				{
					Directory.Delete(item, true);
				}
			}

			if (waitWindow != null)
			{
				waitWindow.Close();
			}
		}

		public static async Task UpdateEmbeddedGrocyRelease(Form ownerFormReference = null)
		{
			FrmWait waitWindow = null;
			if (ownerFormReference != null)
			{
				waitWindow = new FrmWait();
				waitWindow.Show(ownerFormReference);
			}

			if (Directory.Exists(GrocyExecutingPath))
			{
				Directory.Delete(GrocyExecutingPath, true);
			}

			string grocyZipPath = Path.GetTempFileName();
			using (WebClient wc = new WebClient())
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Downloading latest grocy release...");
				}
				await wc.DownloadFileTaskAsync(new Uri(LATEST_GROCY_RELEASE_URL), grocyZipPath);
			}

			if (waitWindow != null)
			{
				waitWindow.SetStatus("Preparing grocy...");
			}
			await Task.Run(() => Extensions.ExtractZipToDirectory(grocyZipPath, GrocyExecutingPath, true));
			File.Delete(grocyZipPath);

			if (waitWindow != null)
			{
				waitWindow.Close();
			}
		}

		public static async Task UpdateEmbeddedBarcodeBuddyRelease(Form ownerFormReference = null)
		{
			FrmWait waitWindow = null;
			if (ownerFormReference != null)
			{
				waitWindow = new FrmWait();
				waitWindow.Show(ownerFormReference);
			}

			if (Directory.Exists(BarcodeBuddyExecutingPath))
			{
				Directory.Delete(BarcodeBuddyExecutingPath, true);
			}

			string barcodeBuddyZipPath = Path.GetTempFileName();
			using (WebClient wc = new WebClient())
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Downloading latest Barcode Buddy release...");
				}
				await wc.DownloadFileTaskAsync(new Uri(LATEST_BARCODE_BUDDY_RELEASE_URL), barcodeBuddyZipPath);
			}

			if (waitWindow != null)
			{
				waitWindow.SetStatus("Preparing Barcode Buddy...");
			}
			await Task.Run(() => Extensions.ExtractZipToDirectory(barcodeBuddyZipPath, BarcodeBuddyExecutingPath + "-tmp", true));
			Directory.Move(Directory.GetDirectories(BarcodeBuddyExecutingPath + "-tmp").First(), BarcodeBuddyExecutingPath);
			Directory.Delete(BarcodeBuddyExecutingPath + "-tmp", true);
			File.Delete(barcodeBuddyZipPath);

			if (waitWindow != null)
			{
				waitWindow.Close();
			}
		}
	}
}
