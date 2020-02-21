using Helper;
using Helper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class Main : Form
    {
        Task<List<Room>> task;
        public Main()
        {
            InitializeComponent();
            Task<string> task1 = Task.Factory.StartNew(() => Helper.SystemHelper.GetAction("Room",true));
            task=task1.ContinueWith<List<Room>>((res) => SystemHelper.GenerateRoomsList(task1.Result));
        }



        private void Menu_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 60;
            }
            else
                MenuVertical.Width = 250;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Maximize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void NormalState_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void ChangePosition(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void StartWithPanel(object form)
        {
            if (this.PanelContainer.Controls.Count > 0)
                this.PanelContainer.Controls.RemoveAt(0);
            Form fh = form as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.PanelContainer.Controls.Add(fh);
            this.PanelContainer.Tag = fh;
            fh.Show();
        }
        private void StartWithPanel(object sender, EventArgs e)
        {
            StartWithPanel(new MainView());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartWithPanel(null,e);
        }

        private void RoomsBtn_Click(object sender, EventArgs e)
        {
            if (task.IsCompleted)
            {
                StartWithPanel(new RoomView(task.Result));
            }
        }
    }
}
