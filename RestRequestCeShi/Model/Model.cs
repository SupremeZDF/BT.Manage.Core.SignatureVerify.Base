using System;
using System.Collections.Generic;
using System.Text;

namespace RestRequestCeShi
{
    public class Model
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Psswd { get; set; }

        public string Sex { get; set; }

        public List<TwoGrades> grade { get; set; }
    }

    public class TwoGrades
    {
        public int ID { get; set; }

        public string Nmae { get; set; }
    }
}
