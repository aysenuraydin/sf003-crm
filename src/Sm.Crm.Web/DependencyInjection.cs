using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Net.Http.Headers;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Infrastructure.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebMvcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddScoped<IUser, CurrentUser>();
        services.AddHttpContextAccessor();

        // ASP.NET Identity yapısını kullandığımız için Cookie Authentication kapatıldı.
        //services
        //    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie(options =>
        //    {
        //        options.LoginPath = "/App/Account/Login";
        //    });

        // Authentication bilgileri Client Secret üzerinden alıyoruz
        services
            .AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"];
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });

        services.AddHttpClient();
        services.AddHttpClient("CrmApi", httpClient =>
        {
            httpClient.BaseAddress = new Uri(configuration["APIs:CrmApi"]);
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        services.AddHttpClient("CanliDovizApi", httpClient =>
        {
            httpClient.BaseAddress = new Uri(configuration["APIs:CanliDovizApi"]);
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        return services;
    }
}