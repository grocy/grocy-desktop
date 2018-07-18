using System.Windows.Forms;

namespace GrocyDesktop
{
	public partial class FrmWait : Form
	{
		public FrmWait()
		{
			InitializeComponent();
		}

		public void SetStatus(string status)
		{
			this.label_Status.Text = status;
		}
	}
}
