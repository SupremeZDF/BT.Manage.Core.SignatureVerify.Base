using System.Collections.Generic;

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
