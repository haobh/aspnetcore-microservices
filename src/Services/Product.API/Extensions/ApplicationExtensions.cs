using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Product.API.Extensions
{
    /// <summary>
    /// Add thêm thuộc tính cho ứng dụng
    /// </summary>
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapDefaultControllerRoute();
            });
        }
    }
}
