using Helper;
using Helper.Interfaces;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class RoomView :Form ,ILogger
    {
        public delegate void PresentMessage(List<Room> Rooms);
        public PresentMessage Presenter;
        List<Room> Rooms;
        public RoomView(List<Room> rooms)
        {
            InitializeComponent();
            Presenter = new PresentMessage(PresentData);
            Rooms = rooms;
        }

        private void PresentData(List<Room> Rooms)
        {
            RoomsTable.DataSource = Rooms;
        }

        private void RoomView_Load(object sender, EventArgs e)
        {
            RoomsTable.DataSource = Rooms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var insertRoom = new InsertNewRoom();
            ViewPanel.Controls.Add(insertRoom);
            insertRoom.BringToFront();
        }
        private void LoadDataFromApi(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.GetAction("Room",this,true))).ContinueWith((res) => this.RoomsTable.Invoke(Presenter, res.Result));
            RoomsTable.BringToFront();
        }

        public void LogErrorToUser(Exception ex)
        {
            MessageBox.Show("Failed To Refresh !!!! Please Check You Connection EX" , "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (RoomsTable.CurrentRow!=null)
            {
                string id = SystemHelper.EncryptContent(RoomsTable.CurrentRow.Cells[0].Value.ToString());
                Task.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.DeleteAction("Room", this, id, true))).ContinueWith((res) => this.RoomsTable.Invoke(Presenter, res.Result));
            }
        }
    }
}
