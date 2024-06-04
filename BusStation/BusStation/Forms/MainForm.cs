using BusStation.DataAccess;
using BusStation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusStation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void test_Click(object sender, EventArgs e)
        {
            UserAccess a = new UserAccess();
            var b = a.GetAll();
        }

        private void insert_Click(object sender, EventArgs e)
        {
           BusAccess db = new BusAccess();
            Bus bus = new Bus { Seats = Convert.ToInt32(textBox1.Text) };
           }

    }
}
