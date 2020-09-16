using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Abp.AspNetCore.Mvc.Authorization
{
    public class AbpAuthorizationFilter : IAsyncAuthorizationFilter, ITransientDependency
    {
        //public ILogger Logger { get; set; }

        //private readonly IAuthorizationHelper _authorizationHelper;
        //private readonly IErrorInfoBuilder _errorInfoBuilder;
        //private readonly IEventBus _eventBus;

        //public AbpAuthorizationFilter(
        //    IAuthorizationHelper authorizationHelper,
        //    IErrorInfoBuilder errorInfoBuilder,
        //    IEventBus eventBus)
        //{
        //    _authorizationHelper = authorizationHelper;
        //    _errorInfoBuilder = errorInfoBuilder;
        //    _eventBus = eventBus;
        //    Logger = NullLogger.Instance;
        //}


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var endpoint = context?.HttpContext?.GetEndpoint();
            // Allow Anonymous skips all authorization
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                return;
            }

            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            ////TODO: Avoid using try/catch, use conditional checking
            //try
            //{
            //    //await _authorizationHelper.AuthorizeAsync(
            //    //    context.ActionDescriptor.GetMethodInfo(),
            //    //    context.ActionDescriptor.GetMethodInfo().DeclaringType
            //    //);
            //}
            //catch (AbpAuthorizationException ex)
            //{
            //    Logger.Warn(ex.ToString(), ex);

            //    _eventBus.Trigger(this, new AbpHandledExceptionData(ex));

            //    if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            //    {
            //        context.Result = new ObjectResult(new AjaxResponse(_errorInfoBuilder.BuildForException(ex), true))
            //        {
            //            StatusCode = context.HttpContext.User.Identity.IsAuthenticated
            //                ? (int)System.Net.HttpStatusCode.Forbidden
            //                : (int)System.Net.HttpStatusCode.Unauthorized
            //        };
            //    }
            //    else
            //    {
            //        context.Result = new ChallengeResult();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(ex.ToString(), ex);

            //    _eventBus.Trigger(this, new AbpHandledExceptionData(ex));

            //    if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            //    {
            //        context.Result = new ObjectResult(new AjaxResponse(_errorInfoBuilder.BuildForException(ex)))
            //        {
            //            StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
            //        };
            //    }
            //    else
            //    {
            //        //TODO: How to return Error page?
            //        context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.InternalServerError);
            //    }
            //}
        }
    }
}
