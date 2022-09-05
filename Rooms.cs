using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace ADO_Project
{
    public partial class Rooms : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-MDMAMUN;Initial Catalog=HManagementDB2;Integrated Security=True");

        public object Value { get; private set; }

        public Rooms()
        {
            InitializeComponent();
            LoadCombo();
            populate();
        }

        private void Rooms_Load(object sender, EventArgs e)
        {

        }
        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Id,TypeName from RoomType", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbType.DataSource = ds.Tables[0];
            cmbType.DisplayMember = "TypeName";
            cmbType.ValueMember = "Id";
            con.Close();

        }
       private void populate()
        {
            con.Open();
            string Query = "select * from HotelRooms";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RoomsDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnpic_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtpicfile.Text = openFileDialog1.FileName;

            }
        }
        private void LoadCategory()
        {
            if (txtName.Text=="" || cmdStatus.Text=="" || cmbType.SelectedIndex ==-1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Image img = Image.FromFile(txtpicfile.Text);
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Bmp);

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into HotelRooms(RoomName,RStatus,roomType,Pictures)values(@N,@S,@T,@P)",con);
                    cmd.Parameters.AddWithValue("@N", txtName.Text);
                    cmd.Parameters.AddWithValue("@S", "Avilable");
                    cmd.Parameters.AddWithValue("@T", cmbType.SelectedValue.ToString());
                    cmd.Parameters.Add(new SqlParameter("@P", SqlDbType.VarBinary) { Value = ms.ToArray() });
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rooms Types Added!!!");

                    con.Close();
                    populate();




                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message + "\n Data not saved");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            LoadCategory();
        }
        int key = 0;

        private void RoomsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = RoomsDGV.SelectedRows[0].Cells[1].Value.ToString();  
            cmdStatus.Text = RoomsDGV.SelectedRows[0].Cells[2].Value.ToString();
            cmbType.Text = RoomsDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtpicfile.Text = RoomsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (txtName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(RoomsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || cmdStatus.Text == "" || cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Image img = Image.FromFile(txtpicfile.Text);
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Bmp);

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update HotelRooms set RoomName = @N, RStatus = @S, roomType =@T,Pictures=@P where RoomID = @key", con);
                    cmd.Parameters.AddWithValue("@N", txtName.Text);
                    cmd.Parameters.AddWithValue("@S", cmdStatus.Text);
                    cmd.Parameters.AddWithValue("@T", cmbType.SelectedIndex.ToString());
                    cmd.Parameters.Add(new SqlParameter("@P", SqlDbType.VarBinary) { Value = ms.ToArray() });
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rooms Updated Successfully!!!");

                    con.Close();
                    populate();




                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message + "\n Data not Updated");
                }
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Select a Room to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from HotelRooms where RoomID = @key", con);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Information Deleted!!!");
                    con.Close();
                    populate();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
