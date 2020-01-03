using System;
using System.Collections.Generic;
using System.Text;

namespace RestRequestCeShi
{
    public class T_User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Psswd { get; set; }

        public string Sex { get; set; }

        public TwoGrade grade { get; set; }
    }

    public class TwoGrade
    {
        public int ID { get; set; }

        public string Nmae { get; set; }
    }
}
