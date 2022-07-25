using SwapFietsDemo.Api.Constants;
using SwapFietsDemo.Api.Services;

namespace SwapFietsDemo.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddSerilog(configuration);

        services.AddScoped<IBikeSearchService, BikeSearchService>();

        var bikeApiUrl = configuration.GetValue<string>("BikeApiUrl");
        
        services.AddHttpClient<IBikeSearchService, BikeSearchService>(opt =>
        {
            opt.BaseAddress = new Uri(bikeApiUrl);
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy(name: ApplicationConstants.ApplicationDefaultCorsPolicy,
                policy  =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}