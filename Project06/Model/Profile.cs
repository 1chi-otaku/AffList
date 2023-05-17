using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project06.Model
{
    public class AProfile
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public AProfile() { }
        public AProfile(string Name, string Description) {
        
            this.Name = Name;
            this.Description = Description;
        }
    }
}
