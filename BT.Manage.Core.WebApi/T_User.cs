namespace BT.Manage.Core.WebApi
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


    public class OneT_User
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
