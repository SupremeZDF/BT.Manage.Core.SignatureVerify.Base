using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.AspNet.Token.Base
{
    public class Result<T> where T : class, new()
    {
        public int code { get; set; }

        public string message { get; set; }

        public T @object { get; set; }
    }
}
