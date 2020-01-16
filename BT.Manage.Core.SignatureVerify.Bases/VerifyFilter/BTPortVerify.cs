using BT.Manage.Frame.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BT.Manage.Core.SignatureVerify.Base
{
    public class BTPortVerify : ActionFilterAttribute
    {

        /// <summary>
        ///  密钥
        /// </summary>
        private readonly static string BTApiKey = ReadJsonConfig.GetConfig().GetSettingNode("ApiKey");

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Result result = new Result();
            HttpRequest request = context.HttpContext.Request;

            //根据请求类型获取参数
            string method = context.HttpContext.Request.Method;
            //时间戳
            long timespan = 0;
            //签名
            string signature = string.Empty;
            if (request.Path.HasValue)
            {
                Dictionary<string, string> RequestPar = new Dictionary<string, string>();
                if (method == "POST")
                {
                    //在内存中创建缓冲区存放Request.Body的内容，从而允许反复读取Request.Body的Stream canseek 是否可以访问流中的某个位置,正文可以多次读取多次
                    request.EnableBuffering();
                    //重新定义流的位置,读取全部的数据
                    request.Body.Position = 0;
                    var postRequestStream = new StreamReader(request.Body, Encoding.Default);
                    var postData = postRequestStream.ReadToEnd();
                    //重置指针 流已到尾部
                    request.Body.Position = 0;
                    //关闭流
                    postRequestStream.Close();
                    string jsonData = postData.TrimStart('"').TrimEnd('"').Replace(@"\", "");
                    //获取 post 请求参数集合
                    if (jsonData.Contains("&") || jsonData.Contains("="))
                    {

                        jsonData.FormStringToDic(ref RequestPar);
                    }
                    else
                    {
                        jsonData.ReqParamesToDic(ref RequestPar);
                    }
                }
                if (method == "GET")

                {
                    var GetRequestPar = request.Query.Keys.ToArray();
                    //var getForm = request.;

                    foreach (var item in GetRequestPar)
                    {
                        var val = request.Query[item].ToString();
                        if (val.IndexOf("'") == -1 || val.IndexOf("{") != -1)
                        {
                            RequestPar.Add(item, val);
                            continue;
                        }
                        var valRelase = val.Replace("'", "");
                        RequestPar.Add(item, valRelase);
                    }
                }

                //校验 时间戳、签名 参数是否有误、缺失
                if (HeaderParameterVerify(context, out timespan, out signature))
                {
                    //请求参数拼接 按照 key1+value1+key2+value2 的方式拼接
                    string RequestParJoint = RequestPar.GetQueryString();
                    //按按 密钥+参数+时间戳 字符串拼接
                    string MD5EncryptStr = BTPardispose.LinkString(BTApiKey, RequestParJoint, timespan.ToString());
                    //验证时间戳是否过期
                    if (timespan.TimeSpan())
                    {
                        //MD5加密 32位
                        string Md5Str = MD5EncryptStr.MD5Encrypt();
                        if (Md5Str == signature)
                        {
                            base.OnActionExecuting(context);
                        }
                        else
                        {
                            context.ExecutingExtend("签名校验失败,请求参数可能被篡改", StatusCodeEnum.SignatureFailure);
                            return;
                        }
                    }
                    else
                    {
                        context.ExecutingExtend("请求已超时", StatusCodeEnum.TimeSpanTimeOut);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// HttpRequest 校验 头部参数是否缺失或为空
        /// </summary>
        /// <param name="context"></param>
        public bool HeaderParameterVerify(ActionExecutingContext context, out long timeSpan, out string signature)
        {
            timeSpan = 0;
            signature = "";
            if (!context.HttpContext.Request.Headers.ContainsKey("timespan") || context.HttpContext.Request.Headers["timespan"].ToString() == "")
            {
                context.ExecutingExtend("缺少时间戳参数或参数为空", StatusCodeEnum.TimeSpanDeletion);
                return false;
            }
            else if (!context.HttpContext.Request.Headers.ContainsKey("signature") || context.HttpContext.Request.Headers["signature"].ToString() == "")
            {
                context.ExecutingExtend("缺少签名参数或参数为空", StatusCodeEnum.SignatureDeleTion);
                return false;
            }
            timeSpan = long.Parse(context.HttpContext.Request.Headers["timespan"].ToString());
            signature = context.HttpContext.Request.Headers["signature"].ToString();
            if (timeSpan.ToString().Length != 13)
            {
                context.ExecutingExtend("时间戳格式有误", StatusCodeEnum.TimeSpanError);
                return false;
            }
            return true;
        }
    }
}
