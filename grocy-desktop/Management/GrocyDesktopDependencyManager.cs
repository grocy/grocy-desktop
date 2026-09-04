using GrocyDesktop.Helpers;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrocyDesktop.Management
{
	public class GrocyDesktopDependencyManager
	{
		private GrocyDesktopDependencyManager()
		{ }

		private static ResourceManager ResourceManager = new ResourceManager(typeof(FrmMain));

		public readonly static string CefExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "cef");
		public readonly static string CefCachePath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "cef-cache");
		public readonly static string CefUserDataPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "cef-userdata");
		public readonly static string NginxExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "nginx");
		public readonly static string PhpExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "php");
		public readonly static string GrocyExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "grocy");
		public readonly static string BarcodeBuddyExecutingPath = Path.Combine(Program.RuntimeDependenciesExecutingPath, "barcodebuddy");

		public static async Task UnpackIncludedDependenciesIfNeeded(UserSettings settings, Form ownerFormReference = null)
		{
			FrmWait waitWindow = null;
			if (ownerFormReference != null)
			{
				waitWindow = new FrmWait();
				waitWindow.Show(ownerFormReference);
			}

			string vcredistZipPath = Path.Combine(Program.BaseExecutingPath, "vcredist.zip");

			// CefSharp
			string cefZipPath = Path.Combine(Program.BaseExecutingPath, "cef.zip");
			if (!Directory.Exists(CefExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus(ResourceManager.GetString("STRING_PreparingWebbrowser.Text"));
				}
				await Task.Run(() => IOHelper.ExtractZipToDirectory(cefZipPath, CefExecutingPath, true));
				await Task.Run(() => IOHelper.ExtractZipToDirectory(vcredistZipPath, CefExecutingPath, true));
			}

			// NGINX
			string nginxZipPath = Path.Combine(Program.BaseExecutingPath, "nginx.zip");
			string nginxCustomizationsZipPath = Path.Combine(Program.BaseExecutingPath, "nginx_customizations.zip");
			if (!Directory.Exists(NginxExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus(ResourceManager.GetString("STRING_PreparingWebserver.Text"));
				}
				await Task.Run(() => IOHelper.ExtractZipToDirectory(nginxZipPath, NginxExecutingPath, true));
				await Task.Run(() => IOHelper.ExtractZipToDirectory(nginxCustomizationsZipPath, NginxExecutingPath, true));
			}

			// PHP
			string phpZipPath = Path.Combine(Program.BaseExecutingPath, "php.zip");
			string phpCustomizationsZipPath = Path.Combine(Program.BaseExecutingPath, "php_customizations.zip");
			if (!Directory.Exists(PhpExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus(ResourceManager.GetString("STRING_PreparingPhpRuntime.Text"));
				}
				await Task.Run(() => IOHelper.ExtractZipToDirectory(phpZipPath, PhpExecutingPath, true));
				await Task.Run(() => IOHelper.ExtractZipToDirectory(vcredistZipPath, PhpExecutingPath, true));
				await Task.Run(() => IOHelper.ExtractZipToDirectory(phpCustomizationsZipPath, PhpExecutingPath, true));
				IOHelper.ReplaceInTextFile(Path.Combine(PhpExecutingPath, "php.ini"), "$PHPPATH$", PhpExecutingPath.Replace("\\", "/").TrimEnd('/'));
			}

			// Grocy
			string grocyZipPath = Path.Combine(Program.BaseExecutingPath, "grocy.zip");
			if (!Directory.Exists(GrocyExecutingPath))
			{
				if (waitWindow != null)
				{
					waitWindow.SetStatus(ResourceManager.GetString("STRING_PreparingGrocy.Text"));
				}
				await Task.Run(() => IOHelper.ExtractZipToDirectory(grocyZipPath, GrocyExecutingPath, true));
			}

			// Barcode Buddy
			if (settings.EnableBarcodeBuddyIntegration)
			{
				string barcodeBuddyZipPath = Path.Combine(Program.BaseExecutingPath, "barcodebuddy.zip");
				if (!Directory.Exists(BarcodeBuddyExecutingPath))
				{
					if (waitWindow != null)
					{
						waitWindow.SetStatus(ResourceManager.GetString("STRING_PreparingBarcodeBuddy.Text"));
					}
					await Task.Run(() => IOHelper.ExtractZipToDirectory(barcodeBuddyZipPath, BarcodeBuddyExecutingPath + "-tmp", true));
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
	}
}
