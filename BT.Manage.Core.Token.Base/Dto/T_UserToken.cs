using System;

namespace BT.Manage.AspNet.Token.Base
{
    public class T_UserToken
    {
        public int FID { get; set; }

        public int FUserID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Guid { get; set; }
    }
}
