using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.Filter;
using POS.Utility;

namespace POS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors(PolicyName = "DefaultCorsPolicy")]
    [TokenAuthenticationFilter]
    public class BaseController : ControllerBase
    {
    }
}
