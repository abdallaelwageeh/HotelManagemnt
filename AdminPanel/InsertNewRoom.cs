using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using Helper.Models;
using Newtonsoft.Json;

namespace AdminPanel
{
    public partial class InsertNewRoom : UserControl
    {
        public List<Room> Rooms{ get; set; }
        public InsertNewRoom()
        {
            Task<List<Helper.Models.Type>> task = Task.Factory.StartNew(() => SystemHelper.GenerateTypeList(SystemHelper.GetAction("Type",true)));
            InitializeComponent();
            if (task.IsCompleted)
            {
                TypeId.DataSource = task.Result;
                TypeId.DisplayMember = "Name";
                TypeId.ValueMember = "Id";
            }
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
                Task<List<Room>> task=Task<List<Room>>.Factory.StartNew(()=>SystemHelper.GenerateRoomsList(SystemHelper.PostAction("Room", content,true)));
                var control = this.Parent.Controls[1] as DataGridView;
                if (task.IsCompleted)
                {
                    control.DataSource = task.Result;
                    control.BringToFront();
                }
                else
                {
                    control.BringToFront();
                    this.Dispose();
                }
            }
        }
        private bool ValidateFields()
        {
            if (Number.Text==string.Empty||Capacity.Text==string.Empty||Price.Text==string.Empty)
            {
                return false;
            }
            else
            {
                return true;    
            }
        }
    }
}
