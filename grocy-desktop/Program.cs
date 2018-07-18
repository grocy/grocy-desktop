using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace GrocyDesktop
{
	internal static class Program
	{
		internal static string BaseExecutingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.TrimEnd('\\'));

		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			Application.Run(new FrmMain());
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			new FrmDisplayException((Exception)e.ExceptionObject).Show();
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			new FrmDisplayException(e.Exception).Show();
		}
	}
}
