using LexiconUniversity2026.Persistence;
using LexiconUniversity2026.Persistence.Data;

namespace LexiconUniversity2026.Web.Extensions
{
    public static class WebAppExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<LexiconUniversityContext>();
                                
                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
