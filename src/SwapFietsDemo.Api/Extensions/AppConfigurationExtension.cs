using SwapFietsDemo.Api.Constants;

namespace SwapFietsDemo.Api.Extensions;

public static class AppConfigurationExtension
{
    public static void AddApplicationConfiguration(this WebApplication app)
    {
        app.UseErrorHandling();
        
        app.UseHttpsRedirection();
        
        app.UseRouting();
        
        app.UseCors(ApplicationConstants.ApplicationDefaultCorsPolicy);

        app.UseAuthorization();

        app.MapControllers();
    }
}