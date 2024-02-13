namespace ImoveisApi.Middlewares
{
    public class AutorizacaoMid
    {
        private readonly RequestDelegate _next;

        public AutorizacaoMid(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("UsuarioLogado"))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}
