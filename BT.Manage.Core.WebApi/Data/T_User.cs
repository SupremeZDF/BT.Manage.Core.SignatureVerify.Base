using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BT.Manage.Core.WebApi.Data
{
    public class T_User
    {
        public int calss { get; set; }

        public string classname { get; set; }

        public DepertMmet depertment { get; set; }
    }

    public class DepertMmet
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }
    }
}
