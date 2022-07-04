using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace RuntimeDbChooser.Blazor.Server.Controllers;
public class CustomLogonController : BlazorLogonController {
    protected override void Accept(SimpleActionExecuteEventArgs args) {
        ((BlazorApplication)Application).ServiceProvider.GetRequiredService<IObjectSpaceProviderContainer>().Clear();
        base.Accept(args);
    }
}

