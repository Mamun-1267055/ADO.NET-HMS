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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void roomsTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomsType fe = new RoomsType();
            fe.MdiParent = this;
            fe.Show();
        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rooms fe = new Rooms();
            fe.MdiParent = this;
            fe.Show();
        }

        private void bookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Booking fe = new Booking();
            fe.MdiParent = this;
            fe.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Reports R = new Reports();
            R.MdiParent = this;
            R.Show();

        }
    }
}
