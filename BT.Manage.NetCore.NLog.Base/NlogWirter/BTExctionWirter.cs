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

        public static BTExctionWirter Default { get; private set; }

        static BTExctionWirter()
        {
            Default = new BTExctionWirter(LogManager.GetCurrentClassLogger());
        }

        public void Debug(string msg, params object[] orgs)
        {
            _logger.Debug(msg, orgs);
        }

        public void Debug(string msg, Exception err)
        {
            _logger.Debug(msg, err);
        }

        public void Info(string msg, params object[] orgs)
        {
            _logger.Info(msg, orgs);
        }

        public void Info(string msg, Exception err)
        {
            _logger.Info(msg, err);
        }

        public void Warn(string msg, params object[] orgs)
        {
            _logger.Warn(msg, orgs);
        }

        public void Warn(string msg, Exception err)
        {
            _logger.Warn(msg, err);
        }

        public void Trace(string msg, params object[] orgs)
        {
            _logger.Trace(msg, orgs);
        }

        public void Trace(string msg, Exception err)
        {
            _logger.Trace(msg, err);
        }

        public void Error(string msg, params object[] err)
        {
            _logger.Error(msg, err);
        }

        public void Error(string msg,Exception err) 
        {
            _logger.Error(msg, err);
        }

        public void Fatal(string msg, params object[] orgs)  
        {
            _logger.Fatal(msg, orgs);
        }

        public void Fatal(string msg, Exception err) 
        {
            _logger.Fatal(msg, err);
        }
    }
}
