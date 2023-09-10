namespace WebAPIAutores.Middlewares
{
    public static class LogAnswerMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogAnswerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogAnswerMiddleware>();
        }
    }
    public class LogAnswerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public LogAnswerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpo = context.Response.Body;
                context.Response.Body = ms;

                await _next(context);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpo);
                context.Response.Body = cuerpo;

                _logger.LogInformation(respuesta);
            }
        }
    }
}
