using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Security;
using Microsoft.Extensions.DependencyInjection;
using RuntimeDbChooser.Services;

namespace RuntimeDbChooser.Blazor.Server.Controllers;
public class CustomLogonController : BlazorLogonController {
    protected override void Accept(SimpleActionExecuteEventArgs args) {
        string? targetDataBaseName = ((BlazorApplication)Application).ServiceProvider.GetRequiredService<ILogonParameterProvider>().GetLogonParameters<IDatabaseNameParameter>().DatabaseName?.Name;
        if(string.IsNullOrEmpty(targetDataBaseName)) {
            throw new UserFriendlyException("The DatabaseName is not specified!");
        }
        ((BlazorApplication)Application).ServiceProvider.GetRequiredService<IObjectSpaceProviderContainer>().Clear();
        base.Accept(args);
    }
}

