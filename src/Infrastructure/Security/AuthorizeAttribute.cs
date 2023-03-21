
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Security;

public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
    /// </summary>
    public CustomAuthorizeAttribute() { }


    /// <summary>
    /// Separate roles with comma like ADM,MNG
    /// </summary>
    public CustomAuthorizeAttribute(string roles)
    {
        Roles = roles;
    }

    /// <summary>
    /// Separate roles with comma like ADM,MNG
    /// </summary>
    public CustomAuthorizeAttribute(string roles, string policy)
    {
        Roles = roles;
        Policy = policy;
    }

    /// <summary>
    /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
    /// </summary>
    public string Roles { get; set; }

    /// <summary>
    /// Gets or sets the policy name that determines access to the resource.
    /// </summary>
    public string Policy { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User?)context.HttpContext.Items["User"];

        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        if (!string.IsNullOrEmpty(Roles))
        {
            var _roles = Roles.Split(',');
            var token = (JwtSecurityToken?)context.HttpContext.Items["Token"];
            var role = token?.Claims.First(claim => claim.Type == "role").Value;

            if (!_roles.Any(a => a == role))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
        }

        if (!string.IsNullOrEmpty(Policy))
        {
            // var token = (JwtSecurityToken)context.HttpContext.Items["Token"];
            // var role = token.Claims.First(claim => claim.Type == "role").Value;

            // if (!Roles.Any(a => a == role))
            // {
            //     context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            // }
        }
    }
}
