using Project06.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project06
{
    public partial class Add : Form
    {
        public int xp;
        public Add()
        {
            InitializeComponent();
            xp = 0;
        }
        public ATask ReturnTask()
        {
            ATask task = new ATask(textBox1.Text, textBox2.Text, "Random", xp, Convert.ToInt32(comboBox1.Text));
            return task;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label13.Text = trackBar1.Value.ToString() + "%";
            CalculateEXP();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label14.Text = trackBar2.Value.ToString() + "%";
            CalculateEXP();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            label15.Text = trackBar3.Value.ToString() + "%";
            CalculateEXP();
        }
        private void CalculateEXP()
        {
            xp = Convert.ToInt32((trackBar1.Value * 1.1) * ((trackBar2.Value * 1.2) / 10) * ((trackBar3.Value * 1)/10));
            label16.Text = xp.ToString() + " EXP";
        }
    }
}
