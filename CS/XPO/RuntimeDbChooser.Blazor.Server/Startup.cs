using DevExpress.Blazor.Reporting;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RuntimeDbChooser.Blazor.Server.Services;
using RuntimeDbChooser.Module;
using RuntimeDbChooser.Module.Blazor;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Blazor.Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton(typeof(HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();
            services.AddSingleton<XpoDataStoreProviderAccessor>();
            services.AddScoped<CircuitHandler, CircuitHandlerProxy>();

            services.AddXaf(Configuration, builder => {
                builder.UseApplication<RuntimeDbChooserBlazorApplication>();
                builder.Modules
                    .AddConditionalAppearance()
                    .AddReports()
                    .AddValidation()
                    .Add<RuntimeDbChooserModule>()
                    .Add<RuntimeDbChooserBlazorModule>();
                builder.ObjectSpaceProviders
                    .AddSecuredXpo((serviceProvider, options) => {
                        //Configure the connection string based on logon parameter values.
                        options.ConnectionString = serviceProvider.GetRequiredService<ConnectionStringProvider>().GetConnectionString();
                        var useMemoryStore = false;
                        options.EnablePoolingInConnectionString = !useMemoryStore;
                        options.ThreadSafe = true;
                        options.UseSharedDataStoreProvider = true;
                    })
                    .AddNonPersistent();
                builder.Security
                    .UseIntegratedMode(options => {
                        options.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
                        options.UserType = typeof(ApplicationUser);
                        options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);
                        options.UseXpoPermissionsCaching();
                    })
                    .AddPasswordAuthentication(options => {
                        options.IsSupportChangePassword = true;
                        options.LogonParametersType = typeof(CustomLogonParametersForStandardAuthentication);
                    });
                builder.AddBuildStep(application => {
                    application.ApplicationName = "RuntimeDbChooser";
                    application.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
                    application.DatabaseVersionMismatch += (s, e) => {
                        e.Updater.Update();
                        e.Handled = true;
                    };
                });
            });

            services.AddScoped<ConnectionStringProvider>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.LoginPath = "/LoginPage";
            });

            services.AddAuthorization(options => {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(
                    CookieAuthenticationDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .RequireXafAuthentication()
                        .Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseXaf();
            app.UseDevExpressBlazorReporting();
            app.UseEndpoints(endpoints => {
                endpoints.MapXafEndpoints();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }
    }
}
