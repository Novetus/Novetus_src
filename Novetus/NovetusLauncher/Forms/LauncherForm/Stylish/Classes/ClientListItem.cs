using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovetusLauncher
{
    public class ClientListItem
    {
        public string ClientName { get; set; }

        public override string ToString() { return ClientName; }
    }
}
