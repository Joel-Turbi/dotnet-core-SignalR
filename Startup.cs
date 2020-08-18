using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Hubs;
using api.Models;
using dotnetcoreSignalR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<PruebaContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("PruebaDatabase")));
            services.AddScoped<INotificationService, Notifications> ();
            services.AddControllers ();
            services.AddSignalR ();
            services.AddCors (options => {
                options.AddPolicy ("AllowAllCors", builder => {
                    builder

                        .WithOrigins ("https://localhost:3000")
                        .AllowAnyHeader ()
                        .AllowAnyMethod ()
                        .AllowCredentials ()
                        .SetIsOriginAllowedToAllowWildcardSubdomains ()
                        .SetIsOriginAllowed (delegate (string requestingOrigin) {
                            return true;
                        }).Build ();
                });
            });

        }

        // Se debe implementar la interfaz en configure , para evitar que se ejecute las notificaciones sin control
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, INotificationService notificationService) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();
            app.UseCors ("AllowAllCors");

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
                endpoints.MapHub<ChatHub> ("/chathub");
            });

            // Se configuro para que las notificaciones solo lleguen una vez
            notificationService.config ();
        }
    }
}