using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BT.Manage.Frame.Base;
using RestSharp;
using System.Linq;
using System.Net;
using System.Web;

namespace BT.Manage.Core.SigRestRequest.Base
{
    /// <summary>
    /// Rest 请求工具类
    /// </summary>
    /// <typeparam name="OutPut">返回的数据类型</typeparam>
    public class SignatureRestRquest<OutPut> where OutPut : class, new()
    {
        /// <summary>
        /// 异步post请求 返回Task<Result<OutPut>>
        /// </summary>
        /// <param name="ApiKey">密钥</param>
        /// <param name="jsonBody">post请求json字符串</param>
        /// <param name="RequestUrl">请求地址Url</param>
        /// <param name="paramrHeader">请求头部字典集合</param>
        /// <typeparam name="IntPut">入参json数据对象</typeparam>
        /// <returns></returns>
        public Task<Result<OutPut>> asyncRestRequestPostResult<IntPut>(string ApiKey, string jsonBody, string RequestUrl, Dictionary<string, object> paramrHeader = null) where IntPut : class, new()
        {
            return Task.Run(() =>
            {
                return SendRequest_ReturnResult(ApiKey, jsonBody, Method.POST, RequestUrl, Newtonsoft.Json.JsonConvert.DeserializeObject<IntPut>(jsonBody), paramrHeader);
            });
        }

        /// <summary>
        /// 异步post请求 返回Task<OutPut>
        /// </summary>
        /// <param name="ApiKey">密钥</param>
        /// <param name="jsonBody">post请求json字符串</param>
        /// <param name="RequestUrl">请求地址Url</param>
        /// <param name="paramrHeader">请求头部字典集合</param>
        /// <typeparam name="IntPut">入参json数据对象</typeparam>
        /// <returns></returns>
        public Task<OutPut> asyncRestRequestPost<IntPut>(string ApiKey, string jsonBody, string RequestUrl, Dictionary<string, object> paramrHeader = null) where IntPut : class, new()
        {
            return Task.Run(() =>
            {
                return SendRequest_ReturnT(ApiKey, jsonBody, Method.POST, RequestUrl, Newtonsoft.Json.JsonConvert.DeserializeObject<IntPut>(jsonBody), paramrHeader);
            });
        }

        /// <summary>
        /// 请求返回Result<OutPut> post 请求
        /// </summary>
        /// <typeparam name="IntPut">jsonBody对象</typeparam>
        /// <param name="ApiKey">密钥</param>
        /// <param name="jsonBody">json字符串</param>
        /// <param name="RequestUrl">请求Url</param>
        /// <param name="paramrHeader">请求头部字典集合</param>
        /// <returns></returns>
        public Result<OutPut> RestRequestPostResult<IntPut>(string ApiKey, string jsonBody, string RequestUrl, Dictionary<string, object> paramrHeader = null) where IntPut : class, new()
        {
            return SendRequest_ReturnResult(ApiKey, jsonBody, Method.POST, RequestUrl, Newtonsoft.Json.JsonConvert.DeserializeObject<IntPut>(jsonBody), paramrHeader);
        }

        /// <summary>
        /// 请求返回OutPut post 请求
        /// </summary>
        /// <typeparam name="IntPut">jsonBody对象</typeparam>
        /// <param name="ApiKey">密钥</param>
        /// <param name="jsonBody">json字符串</param>
        /// <param name="RequestUrl">请求Url</param>
        /// <param name="paramrHeader">请求头部字典集合</param>
        /// <returns></returns>
        public OutPut RestRequestPost<IntPut>(string ApiKey, string jsonBody, string RequestUrl, Dictionary<string, object> paramrHeader = null) where IntPut : class, new()
        {
            return SendRequest_ReturnT(ApiKey, jsonBody, Method.POST, RequestUrl, Newtonsoft.Json.JsonConvert.DeserializeObject<IntPut>(jsonBody), paramrHeader);
        }

        /// <summary>
        /// 异步Get请求 返回Task<Result<OutPut>>
        /// </summary>
        /// <param name="ApiKey">密钥</param>
        /// <param name="jsonBody">post请求json字符串</param>
        /// <param name="RequestUrl">请求地址Url</param>
        /// <param name="paramrHeader">请求头部字典集合</param>
        /// <returns></returns>
        public Task<Result<OutPut>> asyncRestRequestGetResult(string ApiKey, string RequestUrl, Dictionary<string, object> paramrHeader = null, Dictionary<string, object> paramData = null)
        {
            return Task.Run(() =>
            {
                return SendRequest_ReturnResult(ApiKey, null, Method.GET, RequestUrl, null, paramrHeader, paramData);
            });
        }

        /// <summary>
        /// 异步Get请求 返回 OutPut
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="paramrHeader"></param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public Task<OutPut> asyncRestRequestGet(string ApiKey, string RequestUrl, Dictionary<string, object> paramrHeader = null, Dictionary<string, object> paramData = null)
        {
            return Task.Run(() =>
            {
                return SendRequest_ReturnT(ApiKey, null, Method.GET, RequestUrl, null, paramrHeader, paramData);
            });
        }

        /// <summary>
        /// Get 请求 返回 Result<OutPut>
        /// </summary>
        /// <param name="ApiKey">密钥</param>
        /// <param name="RequestUrl">请求路径</param>
        /// <param name="paramrHeader">请求头部字典集合</param>
        /// <param name="paramData">请求参数集合</param>
        /// <returns></returns>
        public Result<OutPut> RestRequestGetResult(string ApiKey, string RequestUrl, Dictionary<string, object> paramrHeader = null, Dictionary<string, object> paramData = null)
        {

            return SendRequest_ReturnResult(ApiKey, null, Method.GET, RequestUrl, null, paramrHeader, paramData);
        }

        /// <summary>
        /// Get请求 返回 OutPut
        /// </summary>
        /// <param name="ApiKey">密钥</param>
        /// <param name="RequestUrl">请求路径</param>
        /// <param name="paramrHeader">请求头部集合</param>
        /// <param name="paramData"><请求参数集合</param>
        /// <returns></returns>
        public OutPut RestRequestGet(string ApiKey, string RequestUrl, Dictionary<string, object> paramrHeader = null, Dictionary<string, object> paramData = null)
        {

            return SendRequest_ReturnT(ApiKey, null, Method.GET, RequestUrl, null, paramrHeader, paramData);
        }

        /// <summary>
        /// 请求返回 OutPut
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="jsonBody"></param>
        /// <param name="method"></param>
        /// <param name="Url"></param>
        /// <param name="paramrHeader"></param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public OutPut SendRequest_ReturnT(string ApiKey, string jsonBody, Method method, string Url, object jsonObect = null, Dictionary<string, object> paramrHeader = null, Dictionary<string, object> paramData = null)
        {
            OutPut result = new OutPut();
            try
            {
                var resPonse = Request_Prepare(ApiKey, jsonBody, method, Url, jsonObect, paramrHeader, paramData);
                if (resPonse.StatusCode != HttpStatusCode.OK)
                {
                    return result;
                }
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result<OutPut>>(resPonse.Content).@object;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        /// <summary>
        /// 请求返回 Result<OutPut>
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="jsonBody"></param>
        /// <param name="method"></param>
        /// <param name="Url"></param>
        /// <param name="paramrHeader"></param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public Result<OutPut> SendRequest_ReturnResult(string ApiKey, string jsonBody, Method method, string Url, object jsonObject = null, Dictionary<string, object> paramrHeader = null, Dictionary<string, object> paramData = null)
        {
            Result<OutPut> result = new Result<OutPut>();
            try
            {
                result.code = 0;
                var resPonse = Request_Prepare(ApiKey, jsonBody, method, Url, jsonObject, paramrHeader, paramData);
                if (resPonse.StatusCode != HttpStatusCode.OK)
                {
                    result.message = "请求异常,Url:" + resPonse.ResponseUri + ",异常信息:" + resPonse.ErrorMessage + " ,HttpStatusCode(请求状态):" + resPonse.StatusCode;
                    return result;
                }
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result<OutPut>>(resPonse.Content);
                if (result.code != 1)
                {
                    result.code = 0;
                    return result;
                }
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

        /// <summary>
        /// 发送请求并返回 RestResponse 数据
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="jsonBody"></param>
        /// <param name="method"></param>
        /// <param name="Url"></param>
        /// <param name="paramrHeader"></param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public IRestResponse Request_Prepare(string ApiKey, string jsonBody, Method method, string Url, object jsonObject = null, Dictionary<string, object> paramrHeader = null, Dictionary<string, object> paramData = null)
        {
            Dictionary<string, object> paramJsonBodyDic = new Dictionary<string, object>();
            string parameStr = "";
            //按照key+value的形式计算字符串 Post 请求
            if (jsonBody != null && jsonBody != "")
            {
                jsonBody.JsonBodyStrToDic(ref paramJsonBodyDic);
            }
            //时间戳
            string timeSpan = RestRquestDto.GetimeStamp().ToString();
            RestClient restClient = new RestClient(Url);
            RestRequest request = new RestRequest(method);
            request.AddHeader("timespan", timeSpan);
            //添加请求头
            if (paramrHeader != null)
            {
                if (paramrHeader.Count >= 1)
                {
                    foreach (var i in paramrHeader)
                    {
                        request.AddHeader(i.Key, i.Value.ToString());
                    }
                }
            }
            if (method == Method.POST)
            {
                //获取jsonbody拼接字符串
                parameStr = paramJsonBodyDic.GetQueryString();
                //拼接MD5加密字符串 密钥+参数str+时间戳
                string Md5str = RestRquestDto.LinkString(ApiKey, parameStr, timeSpan);
                string encryptStr = Md5str.MD5Encrypt();
                request.AddHeader("signature", encryptStr);
                //将obj序列化为JSON格式并将其添加到请求体中。 添加请求体
                request.AddJsonBody(jsonObject);
            }
            else
            {
                //拼接MD5加密字符串 密钥+参数str+时间戳
                parameStr = paramData.GetQueryString();
                string Md5str = RestRquestDto.LinkString(ApiKey, parameStr, timeSpan);
                string encryptStr = Md5str.MD5Encrypt();
                request.AddHeader("signature", encryptStr);
                //添加Get请求参数
                if (paramData != null && paramData.Count >= 1)
                {
                    foreach (var paramedata in paramData)
                    {
                        request.AddParameter(paramedata.Key, paramedata.Value.ToString());
                    }
                }
            }
            return restClient.Execute(request);
        }
    }
}
