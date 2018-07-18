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

		public static async Task UnpackIncludedDependenciesIfNeeded(Form ownerFormReference = null)
		{
			FrmWait waitWindow = null;
			if (ownerFormReference != null)
			{
				waitWindow = new FrmWait();
				waitWindow.Show(ownerFormReference);
			}

			//CefSharp x64
			string cefZipPathx64 = Path.Combine(Program.BaseExecutingPath, "cefx64.zip");
			string cefPathx64 = Path.Combine(Program.BaseExecutingPath, "x64");
			if (!Directory.Exists(cefPathx64))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing embedded web browser (x64)...");
				}
				await Task.Run(() => ZipFile.ExtractToDirectory(cefZipPathx64, cefPathx64));
			}

			//CefSharp x86
			string cefZipPathx86 = Path.Combine(Program.BaseExecutingPath, "cefx86.zip");
			string cefPathx86 = Path.Combine(Program.BaseExecutingPath, "x86");
			if (!Directory.Exists(cefPathx86))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing embedded web browser (x86)...");
				}
				await Task.Run(() => ZipFile.ExtractToDirectory(cefZipPathx86, cefPathx86));
			}

			//PHP
			string phpZipPath = Path.Combine(Program.BaseExecutingPath, "php.zip");
			string phpPath = Path.Combine(Program.BaseExecutingPath, "php");
			if (!Directory.Exists(phpPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing embedded PHP server...");
				}
				await Task.Run(() => ZipFile.ExtractToDirectory(phpZipPath, phpPath));
			}

			//grocy
			string grocyZipPath = Path.Combine(Program.BaseExecutingPath, "grocy.zip");
			string grocyPath = Path.Combine(Program.BaseExecutingPath, "grocy");
			if (!Directory.Exists(grocyPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus("Preparing grocy...");
				}
				await Task.Run(() => ZipFile.ExtractToDirectory(grocyZipPath, grocyPath));
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

			string grocyZipPath = Path.Combine(Program.BaseExecutingPath, "grocy.zip");
			if (File.Exists(grocyZipPath))
			{
				File.Delete(grocyZipPath);
			}

			string grocyPath = Path.Combine(Program.BaseExecutingPath, "grocy");
			if (Directory.Exists(grocyPath))
			{
				Directory.Delete(grocyPath, true);
			}

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
			await Task.Run(() => ZipFile.ExtractToDirectory(grocyZipPath, grocyPath));

			if (waitWindow != null)
			{
				waitWindow.Close();
			}
		}
	}
}
