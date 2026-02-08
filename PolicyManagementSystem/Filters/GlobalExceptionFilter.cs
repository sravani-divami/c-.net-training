using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PolicyManagementSystem.DTOs;
using PolicyManagementSystem.Exceptions;

namespace PolicyManagementSystem.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var traceId = context.HttpContext.TraceIdentifier;
            
            _logger.LogError(context.Exception, "An error occurred. TraceId: {TraceId}", traceId);

            var errorResponse = context.Exception switch
            {
                BaseException baseEx => new ErrorResponseDto
                {
                    ErrorCode = baseEx.ErrorCode,
                    Message = baseEx.Message,
                    TraceId = traceId
                },
                _ => new ErrorResponseDto
                {
                    ErrorCode = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred",
                    TraceId = traceId
                }
            };

            var statusCode = context.Exception switch
            {
                BaseException baseEx => baseEx.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
