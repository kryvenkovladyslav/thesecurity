using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authentication.WebApi
{
    public sealed class Startup
    {
        public IConfiguration Configuration { get; init; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDataProtection();
            //services.AddSimpleCookieSignInOutService(); this line of code is not needed anymore

            services.AddSimpleSignInOutService();
            services.AddAuthentication(options =>
            {
                options.AddScheme<SimpleCookieSignInAuthenticationHandler>
                    (AuthenticationDefaults.CustomCookieAuthentication, AuthenticationDefaults.CustomCookieAuthentication);

                options.DefaultScheme = AuthenticationDefaults.CustomCookieAuthentication;
                options.DefaultAuthenticateScheme = AuthenticationDefaults.CustomCookieAuthentication;
            });

            services.AddAuthorization();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseSimpleCookieAuthentication(); this line of code is not needed anymore
            //app.UseSimpleAuthentication(); this line of code is not needed anymore
            //app.UseClaimsProvider(); this line of code is not needed anymore
            app.UseAuthentication();
            app.UsePrincipalReporter();

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseSimpleAuthorization(); this line of code is not needed anymore
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}