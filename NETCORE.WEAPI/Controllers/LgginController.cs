using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NETCORE.WEAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LgginController : ControllerBase
    {
        [HttpPost]
        public void Run() 
        {
        
        }
    }
}