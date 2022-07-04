<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128588168/20.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1344)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

### Common
* [Updater.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Module/DatabaseUpdate/Updater.cs) (EFCore: [Updater.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Module/DatabaseUpdate/Updater.cs))
* [CustomLogonParameters.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.cs) (EFCore: [CustomLogonParameters.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.cs))

### WinForms
* [Program.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Win/Program.cs)
* [WinApplication.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Win/WinApplication.cs)
* [CustomLogonController.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Win/Controllers/CustomLogonController.cs)

### ASP.NET WebForms
* [WebApplication.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Web/WebApplication.cs)
* [CustomLogonController.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Web/Controllers/CustomLogonController.cs)


### Blazor Server
* [Startup.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Startup.cs) (XPO: [Startup.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Startup.cs))
* [BlazorApplication.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/BlazorApplication.cs) (XPO: [BlazorApplication.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/BlazorApplication.cs))
* [XpoDataStoreProviderAccessor\.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/XpoDataStoreProviderAccessor.cs) (XPO: [XpoDataStoreProviderAccessor.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/XpoDataStoreProviderAccessor.cs))
* [ConnectionStringProvider\.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringProvider.cs) (XPO: [ConnectionStringProvider.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringProvider.cs))
* [ConnectionStringHelper\.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringHelper.cs) (XPO: [ConnectionStringProvider.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringProvider.cs))
* [DbContext\.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Module/BusinessObjects/DbContext.cs)

<!-- default file list end -->

# How to change connection to the database at runtime from the logon form


## Scenario
This example illustrates how to connect your application to another database after the application is already started. This can be required for a multi-tenant application where you need to associate a user or company with their own database of the same structure. You can choose a required database and user during the login procedure. The created databases will have the same structure, but can have a different predefined data set.

![](https://raw.githubusercontent.com/DevExpress-Examples/XAF_how-to-change-connection-to-the-database-at-runtime-e1344/20.2.5%2B/media/e1344Blazor.png)
  
## Implementation Steps
1. Using the Solution Wizard, create a new XAF app named *RuntimeDbChooser* and that uses XPO or EFCore for data access and the Security module with the *Authentication = Standard* and *Integrated Mode* options.
2. Copy code that creates predefined security users for each database from the *RuntimeDbChooser.Module\DatabaseUpdate\Updater.xx* file into *YourSolutionName.Module/DatabaseUpdate/Updater.xx*.
3. Copy and include the *RuntimeDbChooser.Module\BusinessObjects\CustomLogonParameters.xx* file into the *YourSolutionName.Module/BusinessObjects* folder.
4. For WinForms application only. Copy and include the *RuntimeDbChooser.Module\ChangeDatabaseActiveDirectoryAuthentication.xx* file into the *YourSolutionName.Module* project. For more information on this API, see [How to: Use Custom Logon Parameters and Authentication](https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112982.aspx).
5. Copy and include the *RuntimeDbChooser.Wxx\WxxApplicationEx.xx* files into the *YourSolutionName.Wxx* project. Rename `RuntimeDbChooserWindowsFormsApplication` or `RuntimeDbChooserAspNetApplication` or `RuntimeDbChooserBlazorApplication` to your `WxxApplication` descendant's name from the *WxxApplication.xx* file.
6. Replace the line that instantiates your `WinApplication` descendant in the *YourSolutionName.Win/Program.xx* file with the `CreateApplication` method call as shown in the *RuntimeDbChooser.Win/Program.xx* file.
7. In the Application Designer invoked for the *YourSolutionName.Web/WebApplication.xx* file, select the *Authentication Standard* component and set its `LogonParametersType` property to `RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication`.
8. Replace the line that instantiates your `BlazorApplication` in the *YourSolutionName.Blazor.Server/Startup.cs* file and set the `AddPasswordAuthentication.Options.LogonParametersType` property to `RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication`.
9. Copy and include the CustomLogonController.cs file into the application project and register that controller in the Application.CreateLogonController method override. For WinForms and ASP.NET WebForms the controller has one implementation and another for Blazor Server application.


## Important Notes

1. In this example, the available database names are loaded from appsettings.json file for XAF Blazor application
2. For WinForms and ASP.NET WebForms database names are hard-coded in the `MSSqlServerChangeDatabaseHelper` class and supplied to the `DatabaseName`  property editor using the [PredefinedValues](https://documentation.devexpress.com/#eXpressAppFramework/DevExpressExpressAppModelIModelCommonMemberViewItem_PredefinedValuestopic) model option. To populate this list with database names that will be known only at runtime (for instance, read from a configuration file or a database), consider the following options:
  - implement custom property editors and assign them to the `DatabaseName` property using the [Model Editor](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112582): [How to: Supply Predefined Values for the String Property Editor Dynamically](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument113101).
  - customize logon parameters in the `XafApplication.LoggingOn` event handler ([example](https://supportcenter.devexpress.com/ticket/details/t1002457/dynamic-database-name-xaf-blazor)).
2. For WinForms and ASP.NET WebForms this `XafApplication.ConnectionString`-based implementation is designed for a simple scenario when no user and password information is stored in the connection string. Otherwise, the sensitive password information is automatically removed by XAF code from the `XafApplication.ConnectionString` and you cannot rely on this API. In such scenarios, we recommend you remember the original connection string information in the `CreateDefaultObjectSpaceProvider` method of your `XafApplication` descendant (see inside the *YourSolutionName.Wxx/WxxApplication.xx* file) as demonstrated in [the E2829 example](https://supportcenter.devexpress.com/ticket/details/e2829#).
3. For WinForms and ASP.NET WebForms you can find alternative solutions at https://youtu.be/o5t3Nb4zP7A (created by DevExpress MVPs Jose Columbie and Joche Ojeda).

## How it works

The main idea is that when the connection string is selected, all ObjectSpaceProviders from the IObjectSpaceProviderContainer container are removed and an application needs to recreate them again with a new connection string. It's required only for XAF Applications because Web API Service creates new ObjectSpaceProviders per request. You can set a required connection string in the IObjectSpaceProviderFactory service. Or use the approach with the XAF application builder for XPO or set it in the DbContext.OnConfiguring method for EFCore.