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
        public Level level { get; set; }

        public Controller()
        {
            level = new Level();
        }
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
        public AProfile ChangeProfile(string name, string description, int level)
        {
            Profile profile = new Profile();
            profile.SetProfileDetails(name, description, level);
            AProfile Aprofile = null;
            DialogResult rez = profile.ShowDialog();
            if (rez == DialogResult.OK)
            {
                Aprofile = profile.ReturnProfile();
                if (profile != null)
                {
                    return Aprofile;
                }
            }
            return null;
        }

        public Level GetLevel(int exp_amount)
        {
            level.GetExp(exp_amount);
            return level;
        }
        public void SetLevel(Level level)
        {
            this.level= level;
        }
    }
}
