using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BT.Manage.AspNet.Token.Base
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class VerifyTokenAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 授权属性过滤属性
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext) 
        {
            var result = actionContext.Request;
            if (!result.Headers.Contains("AccessToken"))
                return;
            var AccessToken = result.Headers.GetValues("AccessToken").FirstOrDefault();
            var data = BaseDataTable.GetUserID(AccessToken);
            var user = data.@object[0];
            if (user == null || (user.FUserID < 0))
            {
                return;
            }
            if (BaseDataTable.IsDataTimePastDue(Convert.ToDateTime(data.@object[0].EndTime)))
            {
                return;
            }
            base.OnAuthorization(actionContext);
        }
    }
}
