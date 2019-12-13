using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BT.Manage.Base;
using BT.Manage.Frame.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BT.Manage.Core.SignatureVerify.Base
{
    public static class BTPardispose
    {
        /// <summary>
        /// 第一步：参数按照ASCII码从小到大排序
        /// </summary>
        /// <param name="parames">字典排序的集合</param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, string> parames)
        {
            if (parames == null || parames.Count == 0)
            {
                return "";
            }

            //字典排序 参数按照ASCII码从小到大排序
            var vDic = (from objDic in parames orderby objDic.Key ascending select objDic);
            //第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");
            foreach (KeyValuePair<string, string> kv in vDic)
            {
                query.Append(kv.Key);
                query.Append(kv.Value);
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
        /// MD5 32位加密
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
        /// 判断时间戳时否有效
        /// </summary>
        /// <returns></returns>
        public static bool TimeSpan(this long timestamp)
        {
            DateTime dt = GetDateTimeFrom1970Ticks(timestamp);
            //取现在时间
            //DateTime dt1 = DateTime.Now ;
            DateTime dt2 = DateTime.Now.AddMinutes(1);
            if (dt < dt2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 时间戳转为C#格式时间10位
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetDateTimeFrom1970Ticks(this long curSeconds)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(curSeconds);
        }


        /// <summary>
        /// 获取请求参数集合 将请求参数json字符串 按照（key=value）形式解析出来
        /// </summary>
        /// <param name="str">json字符串</param>
        /// <param name="str">字典集合</param>
        public static void ReqParamesToDic(this string str, ref Dictionary<string, string> RequestPar)
        {
            if (str == null || str == "")
                return;
            var jobect = JObject.Parse(str);
            foreach (var child in jobect.Children())
            {
                var proper = child as JProperty;
                RequestPar.Add(proper.Name, proper.Value.ToString());
            }
        }

        /// <summary>
        /// 获取请求参数集合 将请求参数json字符串 按照（key=value）形式解析出来 包括内嵌 json 数据（多层json）
        /// </summary>
        /// <param name="str">json字符串</param>
        /// <param name="str">字典集合</param>
        public static void ReqParamesAllToDic(this string str, ref Dictionary<string, string> RequestPar)
        {
            if (str == null || str == "")
                return;
            Dictionary<string, string> parames = new Dictionary<string, string>();
            var p = JObject.Parse(str) as JToken;
            JToken readercopy = p.DeepClone();
            var reader = readercopy.CreateReader();

            while (reader.Read())
            {
                if (reader.Value != null)
                {

                    if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
                    {
                        parames.Add(reader.Path, reader.Value.ToString());
                    }
                }
            }
            foreach (var parame in parames)
            {
                var keystr = parame.Key.Split(new char[] { '.' });
                if (keystr.Count() == 1)
                {
                    RequestPar.Add(parame.Key, parame.Value);
                    continue;
                }
                RequestPar.Add(keystr[keystr.Count() - 1], parame.Value);
            }
        }

        /// <summary>
        /// ActionFilterAttribute 扩展方法 返回错误信息
        /// </summary>
        /// <param name="action"></param>
        /// <param name="Message">错误信息</param>
        /// <param name="statusCode">返回码</param>
        public static void ExecutingExtend(this ActionExecutingContext action, string Message, StatusCodeEnum statusCode)
        {
            Result result = new Result();
            result.code = (int)statusCode;
            result.message = Message;
            var r = new JsonResult(result);
            r.StatusCode = (int)HttpStatusCode.OK;
            action.Result = r;
        }

    }
}
