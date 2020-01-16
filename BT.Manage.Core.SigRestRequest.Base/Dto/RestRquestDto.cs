using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BT.Manage.Core.SigRestRequest.Base
{
    public static class RestRquestDto
    {
        /// <summary>
        /// 第一步：参数按照ASCII码从小到大排序
        /// </summary>
        /// <param name="parames">字典排序的集合</param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, object> parames)
        {
            if (parames == null || parames.Count == 0)
            {
                return "";
            }

            //字典排序 参数按照ASCII码从小到大排序
            var vDic = (from objDic in parames orderby objDic.Key ascending select objDic);
            //第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");
            foreach (KeyValuePair<string, object> kv in vDic)
            {
                query.Append(kv.Key);
                query.Append(kv.Value.ToString());
            }

            return query.ToString();
        }


        /// <summary>
        /// 按照 密钥 +参数拼接+时间戳 进行参数拼接
        /// </summary>
        /// <param name="ApiKey">密钥</param>
        /// <param name="RequestPar">请求参数拼接</param>
        /// <param name="Timespan">时间戳</param>
        /// <returns></returns>
        public static string LinkString(string ApiKey, string RequestPar, string Timespan)
        {
            return ApiKey + RequestPar + Timespan;
        }

        /// <summary>
        /// 获取请求参数集合 将请求参数json字符串 按照（key=value）形式解析出来
        /// </summary>
        /// 
        /// <param name="str">json字符串</param>
        /// <param name="str">字典集合</param>
        public static void JsonBodyStrToDic(this string str, ref Dictionary<string, object> RequestPar)
        {
            if (str == null || str == "")
                return;
            var jobect = JObject.Parse(str);
            foreach (var child in jobect.Children())
            {
                var proper = child as JProperty;
                if (proper.Value.ToString().Contains("{") || proper.Value.ToString().Contains("{"))
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject(HttpUtility.UrlDecode(proper.Value.ToString()));
                    RequestPar.Add(proper.Name, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    continue;
                }
                //解析socket流 HttpUtility.UrlDecode()解码
                RequestPar.Add(proper.Name, HttpUtility.UrlDecode(proper.Value.ToString()));
            }
        }

        /// <summary>
        /// MD5 32位加密 字母转化为小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt(this string str)
        {
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个MD5
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                pwd += s[i].ToString("X2");
            }
            return pwd.ToLower();
        }

        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
    }
}
