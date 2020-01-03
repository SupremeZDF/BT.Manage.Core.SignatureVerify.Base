using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BT.Manage.Core.WebApi
{
    public class GetCeShi
    {
        public int id { get; set; }

        public string Name { get; set; }

        public List<GetTwoCeShi> Demo { get; set; }
    }

    public class GetTwoCeShi
    {
        public int one { get; set; }

        public string Sign { get; set; }
    }
}
