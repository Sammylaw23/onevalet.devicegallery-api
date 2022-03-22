using OneValet.DeviceGallery.API.Middlewares;

namespace OneValet.DeviceGallery.API.Extensions
{
    public static class AppExtensions
    {
        public static void UseApiErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiErrorHandler>();
        }
    }
}
