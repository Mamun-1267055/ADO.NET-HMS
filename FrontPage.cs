using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_Project
{
    public partial class FrontPage : Form
    {
        public FrontPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            RoomsType rt = new RoomsType();
            rt.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Rooms R = new Rooms();
            R.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Booking B = new Booking();
            B.Show();
        }

        private void FrontPage_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
