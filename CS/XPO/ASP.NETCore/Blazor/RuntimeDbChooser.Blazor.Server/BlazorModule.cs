using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;

namespace RuntimeDbChooser.Module.Blazor;
public sealed class RuntimeDbChooserBlazorModule : ModuleBase {
    public RuntimeDbChooserBlazorModule() { }

    private void Application_CreateCustomUserModelDifferenceStore(object? sender, CreateCustomModelDifferenceStoreEventArgs e) {
        ArgumentNullException.ThrowIfNull(sender);
        e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Blazor");
        e.Handled = true;
    }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        return ModuleUpdater.EmptyModuleUpdaters;
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;
    }
}

