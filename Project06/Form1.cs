using Project06.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project06
{
    public partial class Form1 : Form
    {
        Controller controller;
        List<ATask> tasks;
        bool theme = true;
        public Form1()
        {
            InitializeComponent();
            controller= new Controller();
            tasks= new List<ATask>();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(75, 75);
            imageList.Images.Add(new Bitmap("images/r0.png"));
            imageList.Images.Add(new Bitmap("images/r1.png"));
            imageList.Images.Add(new Bitmap("images/r2.png"));
            imageList.Images.Add(new Bitmap("images/r3.png"));
            imageList.Images.Add(new Bitmap("images/r4.png"));
            imageList.Images.Add(new Bitmap("images/r5.png"));
            imageList.Images.Add(new Bitmap("images/r6.png"));
            imageList.Images.Add(new Bitmap("images/r7.png"));
            listView1.SmallImageList = imageList;
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.None);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ATask task = controller.NewTask();
            if (task != null)
            {
                tasks.Add(task);
                AddTasks();
            }
        }
        private void AddTasks()
        {
            listView1.Items.Clear();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (String.IsNullOrEmpty(tasks[i].task_description)) tasks[i].task_description = "No description available.";
                if (String.IsNullOrEmpty(tasks[i].task_name)) tasks[i].task_name = "Task";

                ListViewItem list_item = new ListViewItem("");
                list_item.SubItems.Add(tasks[i].task_name);
                list_item.SubItems.Add(tasks[i].task_description);
                list_item.SubItems.Add(tasks[i].date);
                list_item.SubItems.Add(tasks[i].exp.ToString());
                list_item.ImageIndex = tasks[i].imageIndex;

                if (i % 2 == 0) list_item.BackColor = Color.Gray;
                else list_item.BackColor = Color.DarkGray;
                listView1.Items.Add(list_item);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            button1.Enabled = true;
            button2.Enabled = false;
            button6.Visible= false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            button1.Enabled = false;
            button2.Enabled = true;
            button6.Visible = true;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = listView1.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }
        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetExp(tasks[listView1.SelectedItems[0].Index].exp);
            tasks.RemoveAt(listView1.SelectedItems[0].Index);
            AddTasks();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GetExp(tasks[listView1.SelectedItems[0].Index].exp);
            tasks.RemoveAt(listView1.SelectedItems[0].Index);
            AddTasks();
            
        }
        private void GetExp(int amount)
        {
            controller.GetLevel(amount);
            label3.Text = controller.level.level_exp.ToString() + "/" + controller.level.next_lvl_exp.ToString();
            label2.Text = controller.level.level.ToString();
            progressBar1.Maximum = controller.level.next_lvl_exp;
            progressBar1.Value = controller.level.level_exp;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tasks.RemoveAt(listView1.SelectedItems[0].Index);
            AddTasks();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AProfile profile = controller.ChangeProfile(label4.Text, label5.Text,controller.level.level);
            if(profile != null ) {
                label4.Text = profile.Name;
                label5.Text = profile.Description;
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(theme == true)
            {
                theme = false;
                this.BackColor = Color.Wheat;
                button1.BackColor = Color.FromArgb(208, 174, 54);
                button2.BackColor = Color.FromArgb(208, 174, 54);
                button3.BackColor = Color.FromArgb(208, 174, 54);
                button4.BackColor = Color.FromArgb(208, 174, 54);
                button5.BackColor = Color.FromArgb(208, 174, 54);
                button6.BackColor = Color.FromArgb(208, 174, 54);
                label1.ForeColor = Color.Black; 
                label2.ForeColor = Color.DarkGreen;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                listView1.BackColor = Color.FromArgb(187, 174, 147);
                tabPage1.BackColor = Color.WhiteSmoke;
            }
            else
            {
                theme = true;
                this.BackColor = Color.FromArgb(32, 32, 32);
            }
        }
    }
}
