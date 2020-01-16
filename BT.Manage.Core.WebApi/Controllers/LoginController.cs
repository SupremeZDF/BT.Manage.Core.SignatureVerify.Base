using BT.Manage.Core.SignatureVerify.Base;
using Microsoft.AspNetCore.Mvc;

namespace BT.Manage.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        [BTPortVerify]
        [HttpPost]
        public string Run()
        {
            return "1111";
        }

        [BTPortVerify]
        [HttpGet]
        public string Con()
        {
            return "123";
        }

        [BTPortVerify]
        [HttpPost]
        public string Form()
        {
            return "sss";
        }

        [BTPortVerify]
        [HttpGet]
        public object PostOneCeshi()
        {
            return "1234";
        }

        [BTPortVerify]
        [HttpPost]
        public object PostTwoCheShi([FromBody]OneT_User t_User)
        {
            return "134";
        }

        [HttpGet]
        [BTPortVerify]
        public object GetThreeCeShi(int name, string pswd, GetCeShi getCeShi)
        {
            return "ss";
        }
    }
}