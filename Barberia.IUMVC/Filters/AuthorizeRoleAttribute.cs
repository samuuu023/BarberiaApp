using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Barberia.IUMVC.Filters
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string[] roles;

        public AuthorizeRoleAttribute(params string[] rolesPermitidos)
        {
            roles = rolesPermitidos;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = context.HttpContext.Session.GetString("usuario");
            var rol = context.HttpContext.Session.GetString("rol");

            if (usuario == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            if (!roles.Contains(rol))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}