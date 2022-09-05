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

namespace ADO_Project
{
   
    public partial class RoomsType : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-MDMAMUN;Initial Catalog=HManagementDB2;Integrated Security=True");
        public RoomsType()
        {
            InitializeComponent();
            show();
        }
        private void show()
        {
            con.Open();
            string Query = "select * from RoomType";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder B = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvTypes.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {

            if (txtType.Text == "" || txtCost.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RoomType(TypeName,Cost)values(@N,@C)", con);
                    cmd.Parameters.AddWithValue("@N", txtType.Text);
                    cmd.Parameters.AddWithValue("@C",txtCost.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rooms Types Added!!!");

                    con.Close();
                    show();




                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message + "\n Data not saved");
                }
            }

        }

        private void Edit()
        {
            if (txtType.Text == "" || txtCost.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update RoomType set TypeName=@N,Cost=@C where Id = @key", con);
                    cmd.Parameters.AddWithValue("@N", txtType.Text);
                    cmd.Parameters.AddWithValue("@C",txtCost.Text);
                    cmd.Parameters.AddWithValue("@key", key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rooms Types Updated!!!");

                    con.Close();
                    show();


                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message + "\n Data not saved");
                }
            }
        }

        int key = 0;
        private void dgvTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtType.Text = dgvTypes.SelectedRows[0].Cells[1].Value.ToString();
            txtCost.Text = dgvTypes.SelectedRows[0].Cells[2].Value.ToString();
            if (txtType.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a Category!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from RoomType where Id = @key", con);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted");
                    con.Close();
                    show();


                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
