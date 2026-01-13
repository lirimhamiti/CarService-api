namespace CarService.Api.Endpoints;

public static class ApiEndpoints
{
    public static WebApplication MapApiEndpoints(this WebApplication app)
    {
        app.MapGarageEndpoints();
        app.MapCarEndpoints();
        app.MapAdminEndpoints();
        app.MapOwnerEndpoints();
        app.MapServiceRecordEndpoints();

        return app;
    }
}
