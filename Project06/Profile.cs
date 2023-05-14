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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project06
{
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        public AProfile ReturnProfile()
        {
            AProfile task = new AProfile(textBox3.Text,textBox2.Text);
            return task;
        }
        public void SetProfileDetails(string name, string description, int level)
        {
            textBox3.Text = name;
            textBox2.Text = description;
            label2.Text = level.ToString(); 
        }
    }
}
