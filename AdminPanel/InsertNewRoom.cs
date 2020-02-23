using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using Helper.Interfaces;
using Helper.Models;
using Newtonsoft.Json;

namespace AdminPanel
{
    public partial class InsertNewRoom : UserControl,ILogger
    {
        public delegate void PresentMessage(List<Helper.Models.Type> types);
        public PresentMessage Presenter;
        public InsertNewRoom()
        {
            Presenter=new PresentMessage(PresentData);
            InitializeComponent();
            var task = Task.Factory.StartNew(() => SystemHelper.GenerateTypeList(SystemHelper.GetAction("Type",true, this))).ContinueWith((res) => TypeId.Invoke(Presenter, res.Result));
        }
        public void PresentData(List<Helper.Models.Type> Types)
        {
            TypeId.DataSource = Types;
            TypeId.DisplayMember = "Name";
            TypeId.ValueMember = "Id";
        }
        private void InsertRoom(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                String content = SystemHelper.EncryptContent(JsonConvert.SerializeObject(
                    new Room 
                    {
                        Number = Number.Text,
                        Capacity = int.Parse(Capacity.Text),
                        Description = Description.Text,
                        Price = Price.Text,
                        TypeId = int.Parse(TypeId.SelectedValue.ToString()),
                        ImagePath = ImagePath.Text
                    }));
                var control = this.Parent.Controls["RoomsTable"] as DataGridView;
                var form=control.Parent.Parent as RoomView;
                var task = Task<List<Room>>.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.PostAction("Room", content,this,true))).ContinueWith((res) => control.Invoke(form.Presenter, res.Result));
                control.BringToFront();
                this.Dispose();
            }
        }
        private bool ValidateFields()
        {
            if (Number.Text==string.Empty||Capacity.Text==string.Empty||Price.Text==string.Empty)
            {
                MessageBox.Show("You Have To Fill All Fields Except Image Field If You Need to Ignore It","Validation",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
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
    }
}
