using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_Project
{
    public partial class Booking : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-MDMAMUN;Initial Catalog=HManagementDB2;Integrated Security=True");
        public Booking()
        {
            InitializeComponent();
            getRooms();
            populate();
        }
        private void getRooms()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from HotelRooms where RStatus='Avilable'", con);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomID", typeof(int));
            dt.Load(rd);
            cmbroom.ValueMember = "RoomID";
            cmbroom.DataSource = dt;
            con.Close();




        }
        int price = 1;
        private void Amount()
        {
            con.Open();
            string Query = "select Cost from HotelRooms join RoomType on roomType=Id where RoomID=" + cmbroom.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query,con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                 price = Convert.ToInt32(dr["Cost"].ToString());
            }
            con.Close();
        }
        //private void getRooms()
        //{
        //    con.Open();
        //    SqlDataAdapter sda = new SqlDataAdapter("select * from HotelRooms where RStatus = 'Avilable'", con);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    cmbroom.DataSource = ds.Tables[0];
        //    cmbroom.DisplayMember = "RoomID";
        //    cmbroom.ValueMember = "RoomID";
        //    con.Close();

        //}
        private void populate()
        {
            con.Open();
            string Query = "select * from Booking";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Bookingdgb.DataSource = ds.Tables[0];
            con.Close();
        }


        private void Booking_Load(object sender, EventArgs e)
        {

        }

        private void cmbroom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Amount();
        }

        private void txtDuration_TextChanged(object sender, EventArgs e)
        {
            if (txtCost.Text == "")
            {
                txtCost.Text = "0";
            }
            else
            {
                try
                {
                    int Total = price * Convert.ToInt32(txtDuration.Text);
                    txtCost.Text = " "+Total;
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message + "\n Data not saved");

                }
            }
            
        }


        private void btnBook_Click(object sender, EventArgs e)
        {
            if (txtDuration.Text == "" || txtName.Text == "" || txtPhone.Text == ""||txtCost.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Booking(RoomID,CustomerName,Phone,CheckIn,Duration,cost)values(@I,@CN,@P,@CD,@D,@C)",con);
                    cmd.Parameters.AddWithValue("@I",cmbroom.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN",txtName.Text);
                    cmd.Parameters.AddWithValue("@P",txtPhone.Text);
                    cmd.Parameters.AddWithValue("@CD",dtp.Value.Date);
                    cmd.Parameters.AddWithValue("@D",txtDuration.Text);
                    cmd.Parameters.AddWithValue("@C",txtCost.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rooms Booking Successfully!!!");
                    con.Close();
                    populate();




                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message + "\n Data not saved");
                }
            }
        }
        int key = 0;
        private void btncancel_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a Booing Number to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Booking where BookId = @key", con);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Booking is Cancled!!!");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Bookingdgb_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           cmbroom.Text = Bookingdgb.SelectedRows[0].Cells[1].Value.ToString();
            txtName.Text = Bookingdgb.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = Bookingdgb.SelectedRows[0].Cells[3].Value.ToString();
            dtp.Text = Bookingdgb.SelectedRows[0].Cells[4].Value.ToString();
            txtDuration.Text = Bookingdgb.SelectedRows[0].Cells[5].Value.ToString();
            txtCost.Text = Bookingdgb.SelectedRows[0].Cells[6].Value.ToString();

            if (txtCost.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(Bookingdgb.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
