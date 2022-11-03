<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128588168/22.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1344)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# XAF - How to Change Connection to the Database at Runtime from the Login Form

## Scenario

This example illustrates how to connect your application to a different database after the application start. Use this approach in a multi-tenant application where you need to associate every user or company with their own database. You can choose the user and the database during the login procedure. In this scenario, all databases have the same structure, but their predefined data sets may vary.

![](https://raw.githubusercontent.com/DevExpress-Examples/XAF_how-to-change-connection-to-the-database-at-runtime-e1344/20.2.5%2B/media/e1344Blazor.png)
  
## Implementation Steps

1. In the Solution Wizard, create a new XAF application and name it _RuntimeDbChooser_.
   * Use either of the available ORM libraries for data access: XPO or EF Core.
   * Select the Security module with the *Authentication = Standard* and *Integrated Mode* options.

2. Copy the code that creates the predefined security users for each database from the _RuntimeDbChooser.Module\DatabaseUpdate\Updater.xx_ file into the _YourSolutionName.Module\DatabaseUpdate\Updater.xx_ file.

3. Copy and include the _RuntimeDbChooser.Module\BusinessObjects\CustomLogonParameters.xx_ file into the *YourSolutionName.Module\BusinessObjects* folder.

4. For WinForms application only. Copy and include the _RuntimeDbChooser.Module\ChangeDatabaseActiveDirectoryAuthentication.xx_ file into the *YourSolutionName.Module* project. For more information on this API, see the following article: [How to: Use Custom Logon Parameters and Authentication](https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112982.aspx).

5. Copy and include the _RuntimeDbChooser.Wxx\WxxApplicationEx.xx_ files into the _YourSolutionName.Wxx_ project. Rename the `RuntimeDbChooserWindowsFormsApplication`, or `RuntimeDbChooserAspNetApplication`, or `RuntimeDbChooserBlazorApplication` to your `WxxApplication` descendant's name from the _WxxApplication.xx_ file.

6. Replace the line that instantiates your `WinApplication` descendant in the _YourSolutionName.Win/Program.xx_ file with the `CreateApplication` method call as shown in the _RuntimeDbChooser.Win/Program.xx_ file.

7. Open the _YourSolutionName.Web/WebApplication.xx_ file in the Application Designer. Select the *Authentication Standard* component and set its `LogonParametersType` property to `RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication`.

8. Replace the line that instantiates your `BlazorApplication` in the _YourSolutionName.Blazor.Server/Startup.cs_ file and set the `AddPasswordAuthentication.Options.LogonParametersType` property to `RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication`.

9. Copy and include the _CustomLogonController.cs_ file into the application project. Register this controller in the `Application.CreateLogonController` method override. The implementation of the controller in WinForms and ASP.NET WebForms applications differs from the implementation in Blazor Server applications.

## Important Notes

1. In this example, XAF Blazor applications load the available database names from the _appsettings.json_ file.

2. In WinForms and ASP.NET WebForms applications, the database names are hard-coded in the `MSSqlServerChangeDatabaseHelper` class and supplied to the `DatabaseName`  property editor using the [PredefinedValues](https://documentation.devexpress.com/#eXpressAppFramework/DevExpressExpressAppModelIModelCommonMemberViewItem_PredefinedValuestopic) model option. To populate this list with database names that become available only at runtime (for example, the application reads the names from a configuration file or a database), consider the following options:

   * Implement custom property editors and assign them to the `DatabaseName` property in the [Model Editor](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112582). For additional information, refer to the following article: [How to: Supply Predefined Values for the String Property Editor Dynamically](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument113101).
   * Customize login parameters in the `XafApplication.LoggingOn` event handler. For additional information, refer to the following example: [Dynamic Database Name XAF Blazor](https://supportcenter.devexpress.com/ticket/details/t1002457/dynamic-database-name-xaf-blazor).

3. For WinForms and ASP.NET WebForms, this `XafApplication.ConnectionString`-based implementation is designed for a simple scenario where the connection string doesn't store the user and password information. Otherwise, XAF removes the sensitive password information from the `XafApplication.ConnectionString` and you cannot rely on this API. In such scenarios, we recommend that you store the original connection string information in the `CreateDefaultObjectSpaceProvider` method of your `XafApplication` descendant (see the *YourSolutionName.Wxx/WxxApplication.xx* file) as demonstrated in the following example: [XAF - How to generate a sequential number for a persistent object within a database transaction](https://supportcenter.devexpress.com/ticket/details/e2829#).

4. For WinForms and ASP.NET WebForms, see the alternative solutions created by DevExpress MVPs Jose Columbie and Joche Ojeda here: [XAF Blazor Change DB at runtime](https://www.youtube.com/watch?v=o5t3Nb4zP7A).

## Files to Review

**Common**
* [Updater.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Module/DatabaseUpdate/Updater.cs) (EFCore: [Updater.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Module/DatabaseUpdate/Updater.cs))
* [CustomLogonParameters.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.cs) (EFCore: [CustomLogonParameters.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.cs))

**WinForms**
* [Program.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Win/Program.cs)
* [WinApplication.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Win/WinApplication.cs)
* [CustomLogonController.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Win/Controllers/CustomLogonController.cs)

**ASP.NET WebForms**
* [WebApplication.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Web/WebApplication.cs)
* [CustomLogonController.cs](./CS/XPO/NET_Framework/RuntimeDbChooser.Web/Controllers/CustomLogonController.cs)

**Blazor Server**
* [Startup.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Startup.cs) (XPO: [Startup.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Startup.cs))
* [BlazorApplication.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/BlazorApplication.cs) (XPO: [BlazorApplication.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/BlazorApplication.cs))
* (XPO: [XpoDataStoreProviderAccessor.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/XpoDataStoreProviderAccessor.cs))
* [ConnectionStringProvider\.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringProvider.cs) (XPO: [ConnectionStringProvider.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringProvider.cs))
* [ConnectionStringHelper\.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringHelper.cs) (XPO: [ConnectionStringProvider.cs](./CS/XPO/ASP.NETCore/Blazor/RuntimeDbChooser.Blazor.Server/Services/ConnectionStringProvider.cs))
* (EFCore [DbContext\.cs](./CS/EFCore/ASP.NETCore/Blazor/RuntimeDbChooser.Module/BusinessObjects/DbContext.cs))
<!-- default file list end -->
