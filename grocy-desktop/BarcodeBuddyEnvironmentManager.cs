using System.IO;

namespace GrocyDesktop
{
	public class BarcodeBuddyEnvironmentManager
	{
		public BarcodeBuddyEnvironmentManager(string basePath)
		{
			this.BasePath = basePath;
		}

		private string BasePath;

		public void Setup(string grocyApiUrl)
		{
			File.WriteAllText(Path.Combine(this.BasePath, "incl\\config.php"), File.ReadAllText(Path.Combine(this.BasePath, "incl\\config.php")).Replace("//\"GROCY_API_URL\"       => null,", "\"GROCY_API_URL\"       => '" + grocyApiUrl + "',"));
		}
	}
}
