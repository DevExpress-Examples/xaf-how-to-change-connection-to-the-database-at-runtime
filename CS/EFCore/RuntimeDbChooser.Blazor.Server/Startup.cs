using DevExpress.Blazor.Reporting;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RuntimeDbChooser.Blazor.Server.Services;
using RuntimeDbChooser.Module;
using RuntimeDbChooser.Module.Blazor;
using RuntimeDbChooser.Module.BusinessObjects;
using RuntimeDbChooser.Services;

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
            services.AddScoped<CircuitHandler, CircuitHandlerProxy>();

            services.AddXaf(Configuration, builder => {
                builder.UseApplication<RuntimeDbChooserBlazorApplication>();
                builder.Modules
                    .AddConditionalAppearance()
                    .AddReports(options => {
                        options.EnableInplaceReports = true;
                        options.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
                        options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
                    })
                    .AddValidation()
                    .Add<RuntimeDbChooserModule>()
                    .Add<RuntimeDbChooserBlazorModule>();
                builder.ObjectSpaceProviders
                    .AddSecuredEFCore().WithDbContext<DemoDbContext>((serviceProvider, options) => {
                        //Configure the connection string based on logon parameter values.
                        //The connection string is assigned dynamically in the DemoDbContext instance.
                        //string connectionString = //...
                        //options.UseSqlServer(connectionString);
                        options.UseLazyLoadingProxies();
                    })
                    .AddNonPersistent();
                builder.Security
                    .UseIntegratedMode(options => {
                        options.RoleType = typeof(PermissionPolicyRole);
                        options.UserType = typeof(ApplicationUser);
                        options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);
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

            services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();

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
