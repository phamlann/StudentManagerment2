using System.Linq;
using System.Web;
using System.Web.Mvc;

public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    private readonly string[] allowedRoles;//mảng chứa các role được phép truy cập

    public AuthorizeByRoleAttribute(params string[] roles)//params cho phép truyền vào nhiều tham số
    {
        this.allowedRoles = roles;//gán giá trị cho mảng allowedRoles
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)//kiểm tra xem người dùng có được phép truy cập không
    {
        if (httpContext.Session["Role"] == null)
        {
            return false;
        }

        var userRole = httpContext.Session["Role"].ToString();//lấy ra role của người dùng
        return allowedRoles.Contains(userRole);
    }
    //xử lý khi người dùng không được phép truy cập
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.Result = new RedirectResult("~/Account/Login");
    }
}
