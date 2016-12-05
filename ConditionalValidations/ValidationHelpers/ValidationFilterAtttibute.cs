using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Text;

namespace ConditionalValidations.ValidationHelpers
{
    public class ValidationFilterAtttibute : ActionFilterAttribute
    {
        //http://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                // Return the validation errors in the response body.
                var errors = new Dictionary<string, IEnumerable<string>>();
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, ModelState> keyValue in actionContext.ModelState)
                {
                    var msg = keyValue.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault();
                    if (msg != null)
                    {
                        errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage);
                        sb.AppendLine(msg);
                    }
                }
                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}