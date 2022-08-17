Imports DevExpress.Blazor.Reporting
Imports DevExpress.ExpressApp.ApplicationBuilder
Imports DevExpress.ExpressApp.Blazor.ApplicationBuilder
Imports DevExpress.ExpressApp.Blazor.Services
Imports DevExpress.ExpressApp.Security
Imports Microsoft.AspNetCore.Authentication.Cookies
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Components.Server.Circuits
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.SignalR
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports RuntimeDbChooser.Blazor.Server.Services
Imports RuntimeDbChooser.Module.BusinessObjects
Imports RuntimeDbChooser.Services

Namespace RuntimeDbChooser.Blazor.Server

    Public Class Startup

        Public Sub New(ByVal configuration As IConfiguration)
            Me.Configuration = configuration
        End Sub

        Public ReadOnly Property Configuration As IConfiguration

        ' This method gets called by the runtime. Use this method to add services to the container.
        ' For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        Public Sub ConfigureServices(ByVal services As IServiceCollection)
            services.AddSingleton(GetType(HubConnectionHandler(Of )), GetType(ProxyHubConnectionHandler(Of )))
            services.AddRazorPages()
            services.AddServerSideBlazor()
            services.AddHttpContextAccessor()
            'services.AddSingleton<XpoDataStoreProviderAccessor>();
            services.AddScoped(Of CircuitHandler, CircuitHandlerProxy)()
            services.AddXaf(Configuration, Function(builder)
                builder.UseApplication(Of RuntimeDbChooserBlazorApplication)()
                builder.Modules.AddConditionalAppearance().AddReports().AddValidation().Add(Of [Module].RuntimeDbChooserModule)().Add(Of RuntimeDbChooserBlazorModule)()
                builder.ObjectSpaceProviders.AddSecuredXpo(Function(serviceProvider, options)
                    'Configure the connection string based on logon parameter values.
                    options.ConnectionString = serviceProvider.GetRequiredService(Of RuntimeDbChooser.Services.IConnectionStringProvider)().GetConnectionString()
                    Dim useMemoryStore = False
                    options.EnablePoolingInConnectionString = Not useMemoryStore
                    options.ThreadSafe = True
                    options.UseSharedDataStoreProvider = True
                End Function).AddNonPersistent()
                builder.Security.UseIntegratedMode(Function(options)
                    options.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
                    options.UserType = GetType([Module].BusinessObjects.ApplicationUser)
                    options.UserLoginInfoType = GetType([Module].BusinessObjects.ApplicationUserLoginInfo)
                    options.UseXpoPermissionsCaching()
                End Function).AddPasswordAuthentication(Function(options)
                    options.IsSupportChangePassword = True
                    options.LogonParametersType = GetType([Module].BusinessObjects.CustomLogonParametersForStandardAuthentication)
                End Function)
                builder.AddBuildStep(Function(application)
                    application.ApplicationName = "RuntimeDbChooser"
                    application.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
                    application.DatabaseVersionMismatch += Function(s, e)
                        e.Updater.Update()
                        e.Handled = True
                    End Function
                End Function)
            End Function)
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Function(options)
                options.LoginPath = "/LoginPage"
            End Function)
            services.AddAuthorization(Function(options)
                options.DefaultPolicy = New AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().RequireXafAuthentication().Build()
            End Function)
            services.AddScoped(Of RuntimeDbChooser.Services.IConnectionStringProvider, ConnectionStringProvider)()
            services.AddSingleton(Of RuntimeDbChooser.Services.IConnectionStringHelper, ConnectionStringHelper)()
        End Sub

        ' This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        Public Sub Configure(ByVal app As IApplicationBuilder, ByVal env As IWebHostEnvironment)
            If env.IsDevelopment() Then
                app.UseDeveloperExceptionPage()
            Else
                app.UseExceptionHandler("/Error")
                ' The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts()
            End If

            app.UseHttpsRedirection()
            app.UseStaticFiles()
            app.UseRouting()
            app.UseAuthentication()
            app.UseAuthorization()
            app.UseXaf()
            app.UseDevExpressBlazorReporting()
            app.UseEndpoints(Function(endpoints)
                endpoints.MapXafEndpoints()
                endpoints.MapBlazorHub()
                endpoints.MapFallbackToPage("/_Host")
                endpoints.MapControllers()
            End Function)
        End Sub
    End Class
End Namespace
