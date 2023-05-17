using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project06.Model
{
    public class Level
    {
        public int level { get; set; }
        public int alltime_exp { get; set; }
        public int level_exp { get; set; }
        public int next_lvl_exp { get; set; }

        public Level()
        {
            level = 1;
            alltime_exp = 0;
            level_exp = 0;
            next_lvl_exp = 1000;
        }

        public Level(int level, int alltime_exp, int level_exp, int next_lvl_exp)
        {
            this.level = level;
            this.alltime_exp = alltime_exp;
            this.level_exp = level_exp;
            this.next_lvl_exp = next_lvl_exp;
        }

        public void GetExp(int amount)
        {
            alltime_exp += amount;
            for (int i = 0; amount != 0; i++)
            {
                if (level_exp + amount >= next_lvl_exp)
                {
                    amount -= next_lvl_exp - level_exp;
                    level_exp = 0;
                    level++;
                    next_lvl_exp *= 2;
                }
                else
                {
                    level_exp += amount;
                    amount = 0;
                }
            }
        }

    }
}
