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
        public RoomView()
        {
            InitializeComponent();
            Presenter = new PresentMessage(PresentData);
            var task = Task.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.GetAction("Room", true, this))).ContinueWith((res) => RoomsTable.Invoke(Presenter, res.Result));
        }

        private void PresentData(List<Room> Rooms)
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
            Task.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.GetAction("Room",true, this))).ContinueWith((res) => this.RoomsTable.Invoke(Presenter, res.Result));
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
                Task.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.DeleteAction("Room", id, this, true))).ContinueWith((res) => this.RoomsTable.Invoke(Presenter, res.Result));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RoomsTable.CurrentRow!=null)
            {
                var room = new Room
                {
                    Id = int.Parse(RoomsTable.CurrentRow.Cells[0].Value.ToString()),
                    Number = RoomsTable.CurrentRow.Cells[1].Value.ToString(),
                    Capacity = int.Parse(RoomsTable.CurrentRow.Cells[3].Value.ToString()),
                    Description = RoomsTable.CurrentRow.Cells[2].Value==null?"": RoomsTable.CurrentRow.Cells[2].Value.ToString(),
                    ImagePath = RoomsTable.CurrentRow.Cells[6].Value == null ? "" : RoomsTable.CurrentRow.Cells[6].Value.ToString(),
                    Price = RoomsTable.CurrentRow.Cells[4].Value.ToString(),
                    TypeId = int.Parse(RoomsTable.CurrentRow.Cells[5].Value.ToString())
                };
                var UpdateRoom = new UpdateRoom(room);
                ViewPanel.Controls.Add(UpdateRoom);
                UpdateRoom.BringToFront();
            }
        }
    }
}
