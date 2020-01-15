using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Manage.AspNet.Token.Base
{
    public class ReadConfig
    {
        /// <summary>
        /// 读取指定的配置文件
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="kry"></param>
        /// <returns></returns>
        public static Result GetKeyValue(string configPath, string key)
        {
            Result result = new Result();
            result.@object = "";
            try
            {
                //配置读取配置文件级别
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = configPath }, ConfigurationUserLevel.None);
                if (configuration.AppSettings.Settings[key] != null)
                {
                    result.@object = configuration.AppSettings.Settings[key].Value;
                    result.code = 1;
                }
                else
                    result.code = 0;
                return result;
            }
            catch (Exception ex)
            {
                result.code = 0;
                result.message = ex.Message;
                return result;
            }
            
        }

        /// <summary>
        /// 设置指定的配置文件
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Result SetkeyValue(string configPath, string key,string values)
        {
            Result result = new Result();
            try
            {
                //配置读取文件级别
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = configPath }, ConfigurationUserLevel.None);
                if (configuration.AppSettings.Settings[key] != null)
                {
                    configuration.AppSettings.Settings[key].Value = values;
                }
                else
                {
                    configuration.AppSettings.Settings.Add(key, values);
                }
                //配置保存模式
                configuration.Save(ConfigurationSaveMode.Modified);
                //更新 配置文件 appSettings 节点
                ConfigurationManager.RefreshSection("appSettings");
                result.code = 1;
                return result;
            }
            catch (Exception ex)
            {
                result.code = 0;
                result.message = ex.Message;
                return result;
            }

        }
    }
}
