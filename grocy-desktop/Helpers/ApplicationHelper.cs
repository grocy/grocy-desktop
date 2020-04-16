using System;
using System.Windows.Forms;

namespace GrocyDesktop.Helpers
{
	public class ApplicationHelper
	{
		private ApplicationHelper()
		{ }

		public static void RestartApp()
		{
			Application.Restart();
			Environment.Exit(0);
		}
	}
}
