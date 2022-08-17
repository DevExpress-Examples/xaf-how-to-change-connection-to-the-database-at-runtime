Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Blazor.DesignTime
Imports DevExpress.ExpressApp.Blazor.Services
Imports DevExpress.ExpressApp.Design
Imports DevExpress.ExpressApp.Utils
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports System
Imports System.Linq
Imports System.Reflection

Namespace RuntimeDbChooser.Blazor.Server

    Public Class Program
        Inherits IDesignTimeApplicationFactory

        Private Shared Function ContainsArgument(ByVal args As String(), ByVal argument As String) As Boolean
            Return args.Any(Function(arg) arg.TrimStart("/"c).TrimStart("-"c).ToLower() Is argument.ToLower())
        End Function

        Public Shared Function Main(ByVal args As String()) As Integer
            If Program.ContainsArgument(args, "help") OrElse Program.ContainsArgument(args, "h") Then
                Console.WriteLine("Updates the database when its version does not match the application's version.")
                Console.WriteLine()
                Console.WriteLine($"    {Assembly.GetExecutingAssembly().GetName().Name}.exe --updateDatabase [--forceUpdate --silent]")
                Console.WriteLine()
                Console.WriteLine("--forceUpdate - Marks that the database must be updated whether its version matches the application's version or not.")
                Console.WriteLine("--silent - Marks that database update proceeds automatically and does not require any interaction with the user.")
                Console.WriteLine()
                Console.WriteLine($"Exit codes: 0 - {DBUpdaterStatus.UpdateCompleted}")
                Console.WriteLine($"            1 - {DBUpdaterStatus.UpdateError}")
                Console.WriteLine($"            2 - {DBUpdaterStatus.UpdateNotNeeded}")
            Else
                DevExpress.ExpressApp.FrameworkSettings.DefaultSettingsCompatibilityMode = DevExpress.ExpressApp.FrameworkSettingsCompatibilityMode.Latest
                Dim host As IHost = CreateHostBuilder(args).Build()
                If Program.ContainsArgument(args, "updateDatabase") Then
                    Using serviceScope = host.Services.CreateScope()
                        Return serviceScope.ServiceProvider.GetRequiredService(Of DevExpress.ExpressApp.Utils.IDBUpdater)().Update(Program.ContainsArgument(args, "forceUpdate"), Program.ContainsArgument(args, "silent"))
                    End Using
                Else
                    host.Run()
                End If
            End If

            Return 0
        End Function

        Public Shared Function CreateHostBuilder(ByVal args As String()) As IHostBuilder
            Return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(Function(webBuilder)
                webBuilder.UseStartup(Of Startup)()
            End Function)
        End Function

        Private Function Create() As XafApplication
            Dim hostBuilder As IHostBuilder = CreateHostBuilder(Array.Empty(Of String)())
            Return DesignTimeApplicationFactoryHelper.Create(hostBuilder)
        End Function
    End Class
End Namespace
