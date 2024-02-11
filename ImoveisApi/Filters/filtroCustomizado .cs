using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ImoveisApi.Filters
{
    public class filtroAutorizacao : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.QueryString.Value.Contains("UsuarioLogado"))

            context.Result = new UnauthorizedObjectResult(new { Menssagem = "Usuário não tem autorização" });
        

            if(context.HttpContext.Request.Query.TryGetValue("usuarioLogado", out var value))
        }

        
    }
}
