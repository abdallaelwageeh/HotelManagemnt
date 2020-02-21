using Helper;
using Helper.Interfaces;
using Helper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class UpdateRoom : UserControl,ILogger
    {
        public delegate void PresentMessage(List<Helper.Models.Type> types);
        public PresentMessage Presenter;
        Room UpdatedRoom;
        public UpdateRoom(Room room)
        {
            UpdatedRoom = room;
            Presenter = new PresentMessage(PresentData);
            InitializeComponent();
            var task = Task.Factory.StartNew(() => SystemHelper.GenerateTypeList(SystemHelper.GetAction("Type", this, true))).ContinueWith((res) => TypeId.Invoke(Presenter, res.Result));
            FillAllFields();
        }
        public void PresentData(List<Helper.Models.Type> Types)
        {
            TypeId.DataSource = Types;
            TypeId.DisplayMember = "Name";
            TypeId.ValueMember = "Id";
        }
        private void FillAllFields()
        {
            Number.Text = UpdatedRoom.Number;
            Description.Text = UpdatedRoom.Description;
            Capacity.Text = UpdatedRoom.Capacity.ToString();
            Price.Text = UpdatedRoom.Price;
            ImagePath.Text = UpdatedRoom.ImagePath;
        }
        private bool ValidateFields()
        {
            if (Number.Text == string.Empty || Capacity.Text == string.Empty || Price.Text == string.Empty)
            {
                MessageBox.Show("You Have To Fill All Fields Except Image Field If You Need to Ignore It", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }
        public void LogErrorToUser(Exception ex)
        {
            MessageBox.Show("Please Check Your Connection", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                var control = this.Parent.Controls["RoomsTable"] as DataGridView;
                var form = control.Parent.Parent as RoomView;
                UpdatedRoom.Number = Number.Text;
                UpdatedRoom.Capacity = int.Parse(Capacity.Text);
                UpdatedRoom.Description = Description.Text;
                UpdatedRoom.Price = Price.Text;
                UpdatedRoom.TypeId = int.Parse(TypeId.SelectedValue.ToString());
                UpdatedRoom.ImagePath = ImagePath.Text;
                String content = SystemHelper.EncryptContent(JsonConvert.SerializeObject(UpdatedRoom));
                var task = Task.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.PutAction("Room", this, content, true))).ContinueWith((res) => control.Invoke(form.Presenter, res.Result));
                control.BringToFront();
                this.Dispose();
            }
        }
    }
}
