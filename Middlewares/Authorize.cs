using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CRM.Interfaces;

public class AuthorizeAttribute : Attribute , IAuthorizationFilter {

    public void OnAuthorization(AuthorizationFilterContext context) {

        var token = context.HttpContext.Request.Cookies["JusTryingToDo"];

        var tokenService = context.HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;

        if (string.IsNullOrEmpty(token) || tokenService?.VerifyTokenGetId(token) == null) {

            var returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            context.HttpContext.Session.SetString("ReturnUrl", returnUrl);
            context.Result = new RedirectToActionResult("Login", "User", null);
        }else {

            context.HttpContext.Items["UserId"] = tokenService.VerifyTokenGetId(token);
        }
    }
}