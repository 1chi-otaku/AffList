using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project06.Model
{
    public class ATask
    {
        public string task_name { get; set; }
        public string task_description { get; set; }
        public string date { get; set; }
        public int imageIndex { get; set; }
        public int exp { get; set; }

        public ATask() { }
        public ATask(string task_name, string description, string date, int exp, int imageIndex)
        {
            this.task_name = task_name;
            this.date = date;
            this.exp = exp;

            if(imageIndex > 15 && imageIndex < 0) imageIndex = 15;
            else this.imageIndex = imageIndex;
            this.task_description = description;
        }
    }
}
