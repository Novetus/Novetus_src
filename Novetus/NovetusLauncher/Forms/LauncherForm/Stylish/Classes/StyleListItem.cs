using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovetusLauncher
{
    public class StyleListItem
    {
        public string StyleName { get; set; }

        public StyleListItem(string name)
        {
            StyleName = name;
        }

        public override string ToString() { return StyleName; }
    }
}
