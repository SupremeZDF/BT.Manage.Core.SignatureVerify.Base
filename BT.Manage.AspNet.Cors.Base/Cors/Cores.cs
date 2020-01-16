using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace BT.Manage.AspNet.Cors.Base
{
    public class Cores : DelegatingHandler
    {
        //源点
        private const string Origin = "Origin";
        //访问控制需要方法
        private const string AccessControlRequestMethod = "Access-Control-Request-Method";
        //访问控制请求头部
        private const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        //访问控制允许原点
        private const string AccessControlAllowOrign = "Access-Control-Allow-Origin";
        //访问控制允许方法
        private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        //访问控制允许头部
        private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        //访问控制允许凭证
        private const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";

        //cancellationToken 多线程取消令牌
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //OPTIONS请求头部中会包含以下头部：Origin、Access-Control-Request-Method、Access-Control-Request-Headers
            bool isCrosRequest = request.Headers.Contains(Origin);
            //XMLHttpRequest 跨域 POST或GET请求 ，请求方式会自动变成OPTIONS的问题 判断是否为 Options 请求
            bool isPrefilightRequest = request.Method == HttpMethod.Options;
            //CORS（cross origin resource share）规范的存在，浏览器会首先发送一次options嗅探，同时header带上origin，判断是否有跨域请求权限，
            //服务器响应access control allow origin的值，供浏览器与origin匹配，如果匹配则正式发送post请求，即便是服务器允许程序跨域访问，
            //若不支持 options 请求，请求也会死掉
            if (isCrosRequest)
            {
                Task<HttpResponseMessage> taskResult = null;
                //判断是否为跨域请求
                if (isPrefilightRequest)
                {
                    taskResult = Task.Factory.StartNew<HttpResponseMessage>(() =>
                    {
                        HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                        httpResponse.Headers.Add(AccessControlAllowOrign, request.Headers.GetValues(Origin).FirstOrDefault());
                        string method = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
                        if (method != null)
                        {
                            httpResponse.Headers.Add(AccessControlAllowMethods, method);
                        }
                        string herders = string.Join(",", request.Headers.GetValues(AccessControlRequestHeaders));
                        if (!string.IsNullOrWhiteSpace(herders))
                        {
                            httpResponse.Headers.Add(AccessControlAllowHeaders, herders);
                        }
                        httpResponse.Headers.Add(AccessControlAllowCredentials, "true");
                        return httpResponse;
                    }, cancellationToken);
                }
                else
                {
                    //多线程
                    taskResult = base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(t =>
                    {
                        var response = t.Result;
                        response.Headers.Add(AccessControlAllowOrign,
                            request.Headers.GetValues(Origin).FirstOrDefault());
                        response.Headers.Add(AccessControlAllowCredentials, "true");
                        return response;
                    });
                }
                return taskResult;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
