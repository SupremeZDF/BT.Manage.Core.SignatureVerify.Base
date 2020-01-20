using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BT.Manage.AspNet.Token.Base;

namespace ASPNET.WEBAPI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [VerifyMvcToken]
        public ActionResult Index()
        {
            HttpContextBase http = this.HttpContext;
            if (HttpContext.Request.Headers["UserID"] == null)
            {
                HttpContext.Response.Redirect("/Home/About");
                return View();
            }
            //获取用户ID
            var userID = int.Parse(HttpContext.Request.Headers["UserID"].ToString());
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Run([System.Web.Http.FromBody]T_User t_User) 
        {
             var t = Base.GetResult(t_User);
            return Newtonsoft.Json.JsonConvert.SerializeObject(t);
           
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        ///演示授权接口
        /// </summary>
        /// <returns></returns>
        [VerifyMvcToken]
        [HttpPost]
        public string YSAuthorizePort() 
        {
            HttpContextBase http = this.HttpContext;
            //如果无UserID 定向与登录页面
            if (HttpContext.Request.Headers["UserID"] == null)
            {
                HttpContext.Response.Redirect("/Home/About");
                return null;
            }
            //获取用户ID
            var userID = int.Parse(HttpContext.Request.Headers["UserID"].ToString());
            //根据用户ID查询数据

            return Newtonsoft.Json.JsonConvert.SerializeObject(new Result()
            {
                code = 1
            });
        }

    }
}