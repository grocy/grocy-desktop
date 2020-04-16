using System.IO;
using System.IO.Compression;

namespace GrocyDesktop.Helpers
{
	public class IOHelper
	{
		private IOHelper()
		{ }

		public static void CopyFolder(string source, string destination)
		{
			foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
			{
				Directory.CreateDirectory(dirPath.Replace(source, destination));
			}

			foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(newPath, newPath.Replace(source, destination), true);
			}
		}

		public static void ExtractZipToDirectory(string archiveFilePath, string destinationDirectory, bool overwrite)
		{
			using (ZipArchive archive = new ZipArchive(File.OpenRead(archiveFilePath)))
			{
				if (!overwrite)
				{
					archive.ExtractToDirectory(destinationDirectory);
					return;
				}

				foreach (ZipArchiveEntry file in archive.Entries)
				{
					string completeFileName = Path.Combine(destinationDirectory, file.FullName);
					string directory = Path.GetDirectoryName(completeFileName);

					if (!Directory.Exists(directory))
					{
						Directory.CreateDirectory(directory);
					}


					if (file.Name != string.Empty)
					{
						file.ExtractToFile(completeFileName, true);
					}
				}
			}
		}

		public static void ReplaceInTextFile(string filePath, string placeholder, string value)
		{
			string text = File.ReadAllText(filePath);
			text = text.Replace(placeholder, value);
			File.WriteAllText(filePath, text);
		}
	}
}
