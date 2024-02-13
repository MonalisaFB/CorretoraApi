namespace ImoveisApi.Middlewares
{
    public class Erros
    {
        private readonly RequestDelegate _next;

        public Erros(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync (HttpContext context, ILogger<Erros> logger)
        {
            try
            {
                await _next(context);
             
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { ErroMensagem = "Erro no sistema" });
                return;
            }
        }
    }
}
