namespace BiblioTech.Middlewares;

public class UnauthorizedMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == 401)
        {
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Unauthorized"
            });
        }
    }
}