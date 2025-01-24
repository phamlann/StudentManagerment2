using System.Linq;
using System.Web;
using System.Web.Mvc;

public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    private readonly string[] allowedRoles;

    public AuthorizeByRoleAttribute(params string[] roles)
    {
        this.allowedRoles = roles;
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        if (httpContext.Session["Role"] == null)
        {
            return false;
        }

        var userRole = httpContext.Session["Role"].ToString();
        return allowedRoles.Contains(userRole);
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.Result = new RedirectResult("~/Account/Login");
    }
}
