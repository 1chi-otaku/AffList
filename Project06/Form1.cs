using Project06.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;
using ListView = System.Windows.Forms.ListView;

namespace Project06
{
    public partial class Form1 : Form
    {
        Controller controller;
        List<ATask> tasks;
        bool theme = true;
        int tasks_completed = 0;
        int tasks_deleted = 0;
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
            imageList.Images.Add(new Bitmap("images/r8.png"));
            imageList.Images.Add(new Bitmap("images/r9.png"));
            imageList.Images.Add(new Bitmap("images/r10.png"));
            imageList.Images.Add(new Bitmap("images/r11.png"));
            imageList.Images.Add(new Bitmap("images/r12.png"));
            imageList.Images.Add(new Bitmap("images/r13.png"));
            imageList.Images.Add(new Bitmap("images/r14.png"));
            imageList.Images.Add(new Bitmap("images/r15.png"));
            listView1.SmallImageList = imageList;
            listView2.SmallImageList = imageList;
            listView3.SmallImageList = imageList;
            listView4.SmallImageList = imageList;


            FileStream stream;
            XmlSerializer serializer;
            if (File.Exists("tasks.xml"))
            {
                stream = new FileStream("tasks.xml", FileMode.Open);
                serializer = new XmlSerializer(typeof(List<ATask>));
                tasks = (List<ATask>)serializer.Deserialize(stream);
                stream.Close();
                FillTasks();
            }
            if (File.Exists("profile_info.xml"))
            {
                stream = new FileStream("profile_info.xml", FileMode.Open);
                serializer = new XmlSerializer(typeof(AProfile));
                AProfile profile = (AProfile)serializer.Deserialize(stream);
                stream.Close();
                label4.Text = profile.Name;
                label5.Text = profile.Description;
            }
            if (File.Exists("lvl_info.xml"))
            {
                stream = new FileStream("lvl_info.xml", FileMode.Open);
                serializer = new XmlSerializer(typeof(Level));
                Level level = (Level)serializer.Deserialize(stream);
                stream.Close();

                progressBar1.Maximum = Int32.MaxValue;
                label2.Text = level.level.ToString();
                progressBar1.Value = level.level_exp;
                progressBar1.Maximum = level.next_lvl_exp;
                label3.Text = progressBar1.Value + "/" + progressBar1.Maximum;

                controller.SetLevel(level);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ATask task = controller.NewTask();
            if (task != null)
            {
                tasks.Add(task);
                FillTasks();
            }
        }
        private void FillTasks()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listView4.Items.Clear();
            for (int i = 0; i < tasks.Count; i++)
            {
                DateTime taskDate = Convert.ToDateTime(tasks[i].date);
                AddTasks(listView1, tasks[i], i % 2 == 0, theme);
                if (taskDate.Date < DateTime.Now.Date)
                    AddTasks(listView4, tasks[i], listView4.Items.Count%2 == 0, theme);
                else if (taskDate.Date == DateTime.Now.Date)
                    AddTasks(listView2, tasks[i], listView2.Items.Count % 2 == 0, theme);
                else if (taskDate.Date == DateTime.Now.Date.AddDays(1))
                    AddTasks(listView3, tasks[i], listView3.Items.Count % 2 == 0, theme);
            }
        }
        private void AddTasks(ListView listView, ATask task, bool isEven, bool theme)
        {
            if (String.IsNullOrEmpty(task.task_description)) task.task_description = "No description available.";
            if (String.IsNullOrEmpty(task.task_name)) task.task_name = "Task";

            ListViewItem list_item = new ListViewItem("");
            list_item.SubItems.Add(task.task_name);
            list_item.SubItems.Add(task.task_description);
            list_item.SubItems.Add(task.date);
            list_item.SubItems.Add(task.exp.ToString());
            list_item.ImageIndex = task.imageIndex;

            if(theme == false)
            {
                if (isEven) list_item.BackColor = Color.FromArgb(187, 174, 147);
                else list_item.BackColor = Color.Wheat;
            }
            else
            {
                if (isEven) list_item.BackColor = Color.Gray;
                else list_item.BackColor = Color.DarkGray;
            }
           
            listView.Items.Add(list_item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            button1.Enabled = true;
            button2.Enabled = false;
            button6.Visible= false;

            statistics_tasks_label = new Label();
            statistics_tasks_label.AutoSize = true;
            statistics_tasks_label.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            statistics_tasks_label.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if(theme) statistics_tasks_label.ForeColor = System.Drawing.Color.Gold;
            else statistics_tasks_label.ForeColor = System.Drawing.Color.DarkGreen;
            statistics_tasks_label.Location = new System.Drawing.Point(200,128);
            statistics_tasks_label.Name = "statistics_label";
            statistics_tasks_label.Size = new System.Drawing.Size(38, 42);
            statistics_tasks_label.TabIndex = 3;
            statistics_tasks_label.Text = "Tasks completed (current session): " + tasks_completed;
            Controls.Add(statistics_tasks_label);

            statistics_deleted_label = new Label();
            statistics_deleted_label.AutoSize = true;
            statistics_deleted_label.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            statistics_deleted_label.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (theme) statistics_deleted_label.ForeColor = System.Drawing.Color.Gold;
            else statistics_deleted_label.ForeColor = System.Drawing.Color.DarkGreen;
            statistics_deleted_label.Location = new System.Drawing.Point(200, 178);
            statistics_deleted_label.Name = "deleted_label";
            statistics_deleted_label.Size = new System.Drawing.Size(38, 42);
            statistics_deleted_label.TabIndex = 3;
            statistics_deleted_label.Text = "Tasks deleted (current session): " + tasks_deleted.ToString();
            Controls.Add(statistics_deleted_label);

            statistics_exp_label = new Label();
            statistics_exp_label.AutoSize = true;
            statistics_exp_label.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            statistics_exp_label.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (theme) statistics_exp_label.ForeColor = System.Drawing.Color.Gold;
            else statistics_exp_label.ForeColor = System.Drawing.Color.DarkGreen;
            statistics_exp_label.Location = new System.Drawing.Point(200, 228);
            statistics_exp_label.Name = "exp_label";
            statistics_exp_label.Size = new System.Drawing.Size(38, 42);
            statistics_exp_label.TabIndex = 3;
            statistics_exp_label.Text = "Exp earned: " + controller.GetLevel(0).alltime_exp.ToString();
            Controls.Add(statistics_exp_label);
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            button1.Enabled = false;
            button2.Enabled = true;
            button6.Visible = true;
            Controls.Remove(statistics_exp_label);
            Controls.Remove(statistics_tasks_label);
            Controls.Remove(statistics_deleted_label);
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
            FillTasks();
            tasks_completed++;
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            doneToolStripMenuItem_Click(sender, e);
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
            tasks_deleted++;
            FillTasks();
        }

        private void button3_Click(object sender, EventArgs e) //Profile button.
        {
            AProfile profile = controller.ChangeProfile(label4.Text, label5.Text,Convert.ToInt32(label2.Text));
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
                listView2.BackColor = Color.FromArgb(187, 174, 147);
                listView3.BackColor = Color.FromArgb(187, 174, 147);
                listView4.BackColor = Color.FromArgb(187, 174, 147);
                tabPage1.BackColor = Color.WhiteSmoke;
                tabPage2.BackColor = Color.WhiteSmoke;
                tabPage3.BackColor = Color.WhiteSmoke;
                tabPage4.BackColor = Color.WhiteSmoke;
                if(statistics_tasks_label != null) statistics_tasks_label.ForeColor = System.Drawing.Color.DarkGreen;
                if (statistics_exp_label != null) statistics_exp_label.ForeColor = System.Drawing.Color.DarkGreen;
                if (statistics_deleted_label != null) statistics_deleted_label.ForeColor = System.Drawing.Color.DarkGreen;
                button7.Image = Image.FromFile("images/night.png");
                FillTasks();
            }
            else
            {
                theme = true;
                this.BackColor = Color.FromArgb(32, 32, 32);
                button1.BackColor = Color.FromArgb(100, 100, 100);
                button2.BackColor = Color.FromArgb(100, 100, 100);
                button3.BackColor = Color.FromArgb(100, 100, 100);
                button4.BackColor = Color.FromArgb(100, 100, 100);
                button5.BackColor = Color.FromArgb(100, 100, 100);
                button6.BackColor = Color.FromArgb(100, 100, 100);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.Gold;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                listView1.BackColor = Color.Gray;
                listView2.BackColor = Color.Gray;
                listView3.BackColor = Color.Gray;
                listView4.BackColor = Color.Gray;
                tabPage1.BackColor = Color.FromArgb(32, 32, 32);
                tabPage2.BackColor = Color.FromArgb(32, 32, 32);
                tabPage3.BackColor = Color.FromArgb(32, 32, 32);
                tabPage4.BackColor = Color.FromArgb(32, 32, 32);
                if (statistics_tasks_label != null) statistics_tasks_label.ForeColor = System.Drawing.Color.Gold;
                if (statistics_exp_label != null) statistics_exp_label.ForeColor = System.Drawing.Color.Gold;
                if (statistics_deleted_label != null) statistics_deleted_label.ForeColor = System.Drawing.Color.Gold;
                button7.Image = Image.FromFile("images/sun.png");
                FillTasks();
            }
        } //Theme changed.

        private void ukrainian_language(object sender, EventArgs e)
        {
            button1.Text = "Завдання!";
            button2.Text = "Статистика!";
            button3.Text = "Профіль!";
            button4.Text = "Рестарт!";
            button5.Text = "Зберегти!";
            button6.Text = "Додати!";

            tabPage1.Text = "Всi";
            tabPage2.Text = "Сьогоднi";
            tabPage3.Text = "Завтра";
            tabPage4.Text = "Просрочено";

            columnHeader1.Text = "Iкон.";
            columnHeader2.Text = "Завдання";
            columnHeader3.Text = "Опис";
            columnHeader4.Text = "Дата";
        }

        private void english_language(object sender, EventArgs e)
        {
            button1.Text = "Tasks!";
            button2.Text = "Statistics!";
            button3.Text = "Profile!";
            button4.Text = "Restart!";
            button5.Text = "Save!";
            button6.Text = "Add Task!";

            tabPage1.Text = "All";
            tabPage2.Text = "Today";
            tabPage3.Text = "Tomorrow";
            tabPage4.Text = "Overdue";


            columnHeader1.Text = "Icon";
            columnHeader2.Text = "Task";
            columnHeader3.Text = "Description";
            columnHeader4.Text = "Date";


        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FileStream stream = null;
            XmlSerializer serializer = null;
            stream = new FileStream("tasks.xml", FileMode.Create);
            serializer = new XmlSerializer(typeof(List<ATask>));
            serializer.Serialize(stream, tasks);
            stream.Close();

            stream = new FileStream("profile_info.xml", FileMode.Create);
            serializer = new XmlSerializer(typeof(AProfile));
            serializer.Serialize(stream, new AProfile(label4.Text, label5.Text));
            stream.Close();

            stream = new FileStream("lvl_info.xml", FileMode.Create);
            serializer = new XmlSerializer(typeof(Level));
            serializer.Serialize(stream, new Level(Convert.ToInt32(label2.Text), Convert.ToInt32(controller.GetLevel(0).alltime_exp.ToString()), progressBar1.Value,progressBar1.Maximum));
            stream.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete your save data?", "Warning!",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                File.Delete("tasks.xml");
                File.Delete("profile_info.xml");
                File.Delete("lvl_info.xml");
                MessageBox.Show("Please restart the app.", "Success!");
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your data?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                button5_Click(sender, e);
            }
        }
    }
}
