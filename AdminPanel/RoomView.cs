using Helper.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class RoomView : Form
    {
        List<Room> Rooms;
        public RoomView(List<Room> rooms)
        {
            InitializeComponent();
            Rooms = rooms;
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
    }
}
