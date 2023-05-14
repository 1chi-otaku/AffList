using Project06.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private void Test()
        {
           
           



            for (int i = 0; i <= 7; i++)
            {
                ListViewItem list_item = new ListViewItem("");
                list_item.SubItems.Add("Приготовить голубичный торт");
                list_item.SubItems.Add("Приготовить голубичный торт с использованием ингридентов");
                list_item.SubItems.Add("5.14.2023");
                list_item.SubItems.Add("250");


                list_item.ImageIndex = i;
                if(i %2 == 0)
                {
                    list_item.BackColor = Color.Gray;
                }
                else
                {
                    list_item.BackColor = Color.DarkGray;
                }
               
                listView1.Items.Add(list_item);
            }
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

    }
}
