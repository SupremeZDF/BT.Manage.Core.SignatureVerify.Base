using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BT.Manage.AspNet.Token.Base;

namespace BT.Manage.AspNet.Token.Base
{
    public class VerifyMvcToken : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var Resquest = httpContext.Request;
            if (Resquest.Headers["AccessToken"] == null || Resquest.Headers.Count == 0)
            {
                return false;
            }
            var AccessToken = Resquest.Headers["AccessToken"].ToString();
            //根据 AccessToken 从数据库中查询数据
            var TokenUser = BaseDataTable.GetUserID(AccessToken);
            //判断是否存在 用户数据
            if (TokenUser.@object == null || TokenUser.@object.Count <= 0) 
            {
                return false;
            }
            //进行数据库操作 如果存在 AccessToken 则进行数据库查询 判断是否否规范
            if (!BaseDataTable.IsDataTimePastDue(TokenUser.@object.FirstOrDefault().EndTime)) 
            {
                return false;
            }
            //添加请求头部
            httpContext.Request.Headers.Add("UserID",TokenUser.@object.FirstOrDefault().FUserID.ToString());
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) 
        {
            //授权失败则进入 登录页面
            filterContext.HttpContext.Response.Redirect("/Home/Index");
        }
    }
}
