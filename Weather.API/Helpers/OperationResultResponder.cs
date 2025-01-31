using Microsoft.AspNetCore.Mvc;
using System.Net;
using Weather.Domain.DTOs;
using Weather.Domain.Enums;

namespace Weather.API.Helpers
{
    public static class OperationResultResponder
    {
        public static IActionResult GetServiceResponse<T>(this ControllerBase controller, OperationResult<T> result)
        {
            var responseCode = GetResponseCode(result.ResultType);
            controller.Response.StatusCode = (int)responseCode;
            if (result.ResultType == OperationResultType.Ok)
            {
                return controller.StatusCode((int)responseCode, result.Value);
            } 
            else
            {
                return controller.StatusCode((int)responseCode, result);
            }
        }

        private static HttpStatusCode GetResponseCode(OperationResultType type)
        {
            switch (type)
            {
                case OperationResultType.Ok:
                    return HttpStatusCode.OK;
                case OperationResultType.NoRecord:
                    return HttpStatusCode.NotFound;
                case OperationResultType.InvalidArguments:
                    return HttpStatusCode.BadRequest;
                default: // OperationResultType.Exception
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
