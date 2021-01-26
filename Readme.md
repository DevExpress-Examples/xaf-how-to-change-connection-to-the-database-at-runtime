<!-- default file list -->
*Files to look at*:

### Common
* [Updater.cs](./CS/RuntimeDbChooser.Module/DatabaseUpdate/Updater.cs) (VB: [Updater.vb](./VB/RuntimeDbChooser.Module/DatabaseUpdate/Updater.vb))
* [CustomLogonParameters.cs](./CS/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.cs) (VB: [CustomLogonParameters.vb](./VB/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.vb))

### WinForms
* [Program.cs](./CS/RuntimeDbChooser.Win/Program.cs) (VB: [Program.vb](./VB/RuntimeDbChooser.Win/Program.vb))
* [WinApplicationEx.cs](./CS/RuntimeDbChooser.Win/WinApplicationEx.cs) (VB: [WinApplicationEx.vb](./VB/RuntimeDbChooser.Win/WinApplicationEx.vb))

### ASP.NET WebForms
* [WebChangeDatabaseController.cs](./CS/RuntimeDbChooser.Module/ChangeDatabaseActiveDirectoryAuthentication.cs) (VB: [WebChangeDatabaseController.vb](./VB/RuntimeDbChooser.Module/ChangeDatabaseActiveDirectoryAuthentication.vb))
* [WebApplication.cs](./CS/RuntimeDbChooser.Web/WebApplication.cs) (VB: [WebApplicationEx.vb](./VB/RuntimeDbChooser.Web/WebApplicationEx.vb))
* [WebApplicationEx.cs](./CS/RuntimeDbChooser.Web/WebApplicationEx.cs) (VB: [WebApplicationEx.vb](./VB/RuntimeDbChooser.Web/WebApplicationEx.vb))

### Blazor Server
* [Startup.cs](./CS/RuntimeDbChooser.Blazor.Server/Startup.cs) and [BlazorApplication.cs](./CS/RuntimeDbChooser.Blazor.Server/BlazorApplication.cs) Please refer to https://github.com/jjcolumb/XAF-blazor-change-connection-to-the-database-at-runtime or watch https://youtu.be/o5t3Nb4zP7A (created by DevExpress MVPs Jose Columbie and Joche Ojeda) for additional approach.

<!-- default file list end -->

# How to change connection to the database at runtime from the logon form


<p><strong>Scenario</strong></p>
<p>This example illustrates how to connect your application to another database after the application is already started. This can be required for a multi-tenant application where you need to associate a user or company with their own database of the same structure. You can choose a required database and user during the login procedure. The created databases will have the same structure, but can have a different predefined data set.</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/XAF_how-to-change-connection-to-the-database-at-runtime-e1344/20.2.5%2B/media/e1344Blazor.png"><br><br><strong>Steps to implement<br></strong></p>
<p>1. Using the XAF Solution Wizard, create a new XAF app named RuntimeDbChooser and that uses XPO for data access and the Security module with the Authentication = Standard and UI-level mode options.<br>2. Copy code that creates predefined security users for each database from the <em>RuntimeDbChooser.Module\DatabaseUpdate\Updater.xx</em> file into <em>YourSolutionName.Module/DatabaseUpdate/Updater.xx</em>.<br>3. Copy and include the <em>RuntimeDbChooser.Module\BusinessObjects\CustomLogonParameters.xx</em> file into the <em>YourSolutionName.Module/BusinessObjects</em> folder.<br>4. Copy and include the <em>RuntimeDbChooser.Module\ChangeDatabaseActiveDirectoryAuthentication.xx</em> file into the <em>YourSolutionName.Module</em> project. For more information, see <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112982.aspx">How to: Use Custom Logon Parameters and Authentication</a>.
<br>5. Copy and include the <em>RuntimeDbChooser.Wxx\WxxApplicationEx.xx</em> files into the <em>YourSolutionName.Wxx</em> project. Change this file's <em>RuntimeDbChooserWindowsFormsApplication</em> or <em>RuntimeDbChooserAspNetApplication</em> or <em>RuntimeDbChooserBlazorApplication</em> class name to your <em>WxxApplication</em> descendant's name from the <em>WxxApplication.xx</em> file.<br>6. Replace the line that instantiates your WinApplication descendant in the YourSolutionName.Win/Program.xx file with the CreateApplication method call as shown in the <em>RuntimeDbChooser.Win/Program.xx file.<br></em>7. In the Application Designer invoked for the YourSolutionName.Web/WebApplication.xx file, select the Authentication Standard component and set its LogonParametersType property to RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication.<br>8. Replace the line that instantiates your BlazorApplication in the YourSolutionName.Blazor.Server/Startup.cs  and set the AddAuthenticationStandard.Options.LogonParametersType property to RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication.</br></p>


## Important Notes

1. In this example, the available database names are hard-coded in the `MSSqlServerChangeDatabaseHelper` class and supplied to the `DatabaseName`  property editor using the [PredefinedValues](https://documentation.devexpress.com/#eXpressAppFramework/DevExpressExpressAppModelIModelCommonMemberViewItem_PredefinedValuestopic) model option. To populate the `PredefinedValues` list with names that will be known only at runtime, implement custom property editors and assign them to the `DatabaseName` property using the [Model Editor](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112582): [How to: Supply Predefined Values for the String Property Editor Dynamically](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument113101).
2. This `XafApplication.ConnectionString`-based implementation is designed for a simple scenario when no user and password information is stored in the connection string. Otherwise, the sensitive password information is automatically removed by XAF code from the `XafApplication.ConnectionString` and you cannot rely on this API. In such scenarios, we recommend you remember the original connection string information in the `CreateDefaultObjectSpaceProvider` method of your `XafApplication` descendant (see inside the YourSolutionName.Wxx/WxxApplication.xx file) as demonstrated in [the E2829 example](https://supportcenter.devexpress.com/ticket/details/e2829#).
