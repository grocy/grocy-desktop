using System.IO;
using System.Xml.Serialization;

namespace GrocyDesktop
{
	public class UserSettings
	{
		private static string SettingsPath = Path.Combine(Program.BaseFixedUserDataFolderPath, "UserSettings.xml");
		public string GrocyDataLocation = Path.Combine(Program.BaseFixedUserDataFolderPath, "grocy-data");
		public string BarcodeBuddyDataLocation = Path.Combine(Program.BaseFixedUserDataFolderPath, "barcodebuddy-data");
		public bool EnableBarcodeBuddyIntegration = false;
		public int GrocyWebserverDesiredPort = 4010;
		public int BarcodeBuddyWebserverDesiredPort = 4011;
		public bool EnableExternalWebserverAccess = false;

		public static UserSettings Load()
		{
			UserSettings settings = new UserSettings();

			if (File.Exists(UserSettings.SettingsPath))
			{
				using (StreamReader reader = new StreamReader(UserSettings.SettingsPath))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
					settings = (UserSettings)serializer.Deserialize(reader);
					reader.Close();
				}
			}

			return settings;
		}

		public void Save()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
			using (TextWriter textWriter = new StreamWriter(UserSettings.SettingsPath))
			{
				serializer.Serialize(textWriter, this);
				textWriter.Close();
			}
		}
	}
}
