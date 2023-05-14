using Project06.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project06
{
    class Controller
    {
        public ATask NewTask()
        {
            Add add = new Add();
            ATask task = null;
            DialogResult rez = add.ShowDialog();
            if (rez == DialogResult.OK)
            {
                task = add.ReturnTask();
                if (task != null)
                {
                    return task;
                }
            }
            return null;
        }
    }
}
