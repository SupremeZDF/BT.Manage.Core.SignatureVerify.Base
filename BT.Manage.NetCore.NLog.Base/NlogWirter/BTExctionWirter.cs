using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace BT.Manage.NetCore.NLog.Base
{
    public class BTExctionWirter
    {
        Logger _logger;

        public BTExctionWirter(Logger logger) 
        {
            _logger = logger;
        }

        public BTExctionWirter(string name) : this(LogManager.GetLogger(name)) 
        {
        
        }

        //public static 
    }
}
