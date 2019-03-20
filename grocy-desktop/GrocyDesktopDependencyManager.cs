using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public class GrocyDesktopDependencyManager
	{
		private const string LATEST_GROCY_RELEASE_URL = "https://releases.grocy.info/latest";

		public readonly static string CefExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "cef");
		public readonly static string CefCachePath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "cef-cache");
		public readonly static string PhpExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "php");
		public readonly static string GrocyExecutingPath = Path.Combine(Program.BaseFixedUserDataFolderPath, "grocy");

		public static async Task UnpackIncludedDependenciesIfNeeded(Form ownerFormReference = null)
		{
			FrmWait waitWindow = null;
			if (ownerFormReference != null)
			{
				waitWindow = new FrmWait();
				waitWindow.Show(ownerFormReference);
			}

			// CefSharp x86
			string cefZipPathx86 = Path.Combine(Program.BaseExecutingPath, "cefx86.zip");
			string cefPathx86 = Path.Combine(CefExecutingPath, "x86");
			if (!Directory.Exists(cefPathx86))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing embedded web browser (x86)...");
				}
				await Task.Run(() => ZipFile.ExtractToDirectory(cefZipPathx86, cefPathx86));
			}

			// PHP
			string phpZipPath = Path.Combine(Program.BaseExecutingPath, "php.zip");
			if (!Directory.Exists(PhpExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing embedded PHP server...");
				}
				await Task.Run(() => ZipFile.ExtractToDirectory(phpZipPath, PhpExecutingPath));
			}

			// grocy
			string grocyZipPath = Path.Combine(Program.BaseExecutingPath, "grocy.zip");
			if (!Directory.Exists(GrocyExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing grocy...");
				}
				await Task.Run(() => ZipFile.ExtractToDirectory(grocyZipPath, GrocyExecutingPath));
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
			await Task.Run(() => ZipFile.ExtractToDirectory(grocyZipPath, GrocyExecutingPath));
			File.Delete(grocyZipPath);

			if (waitWindow != null)
			{
				waitWindow.Close();
			}
		}
	}
}
