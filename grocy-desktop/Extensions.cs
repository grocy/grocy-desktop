using System.IO;

namespace GrocyDesktop
{
	public static class Extensions
	{
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
	}
}
