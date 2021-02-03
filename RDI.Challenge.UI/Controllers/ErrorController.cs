using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RDI.Challenge.UI.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RDI.Challenge.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {

        [Route("{code}")]
        public IActionResult Get(int code)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)code;
            ApiError error = new ApiError(code, parsedCode.ToString());

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = context?.Error; 


            if (exception != null)
                error = new ApiError(403, "Unexpected Error");

            if (exception is Exception)
                error = new ApiError(500, exception.Message);

            if (exception is ArgumentNullException)
                error = new ApiError(500, exception.Message);

            if (exception is SqlException)
                error = new ApiError(500, "Unexpected DataBase Error", exception.Message);

            //else if (exception is N) code = 401; // Unauthorized
            //else if (exception is MyException) code = 400; // Bad Request

            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            Console.WriteLine(error.ToString());

            return new ObjectResult(error);


        }
    }
}
