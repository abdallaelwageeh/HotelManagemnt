using System;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss ");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }
    }
}
