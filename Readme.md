<!-- default file list -->
*Files to look at*:

* [WebChangeDatabaseController.cs](./CS/RuntimeDbChooser.Module.Web/WebChangeDatabaseController.cs) (VB: [WebChangeDatabaseController.vb](./VB/RuntimeDbChooser.Module.Web/WebChangeDatabaseController.vb))
* [WebCustomAuthentication.cs](./CS/RuntimeDbChooser.Module.Web/WebCustomAuthentication.cs) (VB: [WebCustomAuthentication.vb](./VB/RuntimeDbChooser.Module.Web/WebCustomAuthentication.vb))
* [WinChangeDatabaseController.cs](./CS/RuntimeDbChooser.Module.Win/WinChangeDatabaseController.cs) (VB: [WinChangeDatabaseController.vb](./VB/RuntimeDbChooser.Module.Win/WinChangeDatabaseController.vb))
* [WinCustomAuthentication.cs](./CS/RuntimeDbChooser.Module.Win/WinCustomAuthentication.cs) (VB: [WinCustomAuthentication.vb](./VB/RuntimeDbChooser.Module.Win/WinCustomAuthentication.vb))
* [CustomLogonParameters.cs](./CS/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.cs) (VB: [CustomLogonParameters.vb](./VB/RuntimeDbChooser.Module/BusinessObjects/CustomLogonParameters.vb))
* [Updater.cs](./CS/RuntimeDbChooser.Module/DatabaseUpdate/Updater.cs) (VB: [Updater.vb](./VB/RuntimeDbChooser.Module/DatabaseUpdate/Updater.vb))
* [WebApplication.cs](./CS/RuntimeDbChooser.Web/WebApplication.cs) (VB: [WebApplicationEx.vb](./VB/RuntimeDbChooser.Web/WebApplicationEx.vb))
* [WebApplicationEx.cs](./CS/RuntimeDbChooser.Web/WebApplicationEx.cs) (VB: [WebApplicationEx.vb](./VB/RuntimeDbChooser.Web/WebApplicationEx.vb))
* [Program.cs](./CS/RuntimeDbChooser.Win/Program.cs) (VB: [Program.vb](./VB/RuntimeDbChooser.Win/Program.vb))
* [WinApplicationEx.cs](./CS/RuntimeDbChooser.Win/WinApplicationEx.cs) (VB: [WinApplicationEx.vb](./VB/RuntimeDbChooser.Win/WinApplicationEx.vb))
* Blazor Server: please refer to https://github.com/jjcolumb/XAF-blazor-change-connection-to-the-database-at-runtime or watch https://youtu.be/o5t3Nb4zP7A (created by DevExpress MVPs Jose Columbie and Joche Ojeda).
<!-- default file list end -->

# How to change connection to the database at runtime


<p><strong>Scenario</strong></p>
<p>This example illustrates how to connect your application to another database after the application is already started. This can be required for a multi-tenant application where you need to associate a user or company with their own database of the same structure. You can choose a required database and user during the login procedure and also later in the application UI by using the specialized SingleChoiceAction (available for Admin only). The created databases will have the same structure, but can have a different predefined data set.</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-change-connection-to-the-database-at-runtime-e1344/15.2.5+/media/ffd2a3d2-ae0a-11e5-80bf-00155d62480c.png"><br><br><strong>Steps to implement<br></strong></p>
<p>1. Using the XAF Solution Wizard, create a new XAF app named RuntimeDbChooser and that uses XPO for data access and the Security module with the Authentication = Standard and UI-level mode options.<br>2. Copy code that creates predefined security users for each database from the <em>RuntimeDbChooser.Module\DatabaseUpdate\Updater.xx</em> file into <em>YourSolutionName.Module/DatabaseUpdate/Updater.xx</em>.<br>3. Copy and include the <em>RuntimeDbChooser.Module\BusinessObjects\CustomLogonParameters.xx</em> file into the <em>YourSolutionName.Module/BusinessObjects</em> folder.<br>4. Copy and include the <em>RuntimeDbChooser.Module.Web\WebChangeDatabaseController.xx</em> and <em>RuntimeDbChooser.Module.Web\WebCustomAuthentication.xx</em> files into the <em>YourSolutionName.Module.Web</em> project.<br>5. Copy and include the <em>RuntimeDbChooser.Module.Win\WinChangeDatabaseController.xx</em> and <em>RuntimeDbChooser.Module.Win\WinCustomAuthentication.xx</em> files into the <em>YourSolutionName.Module.Win</em> project.<br>6. Copy and include the <em>RuntimeDbChooser.Wxx\WxxApplicationEx.xx</em> files into the <em>YourSolutionName.Wxx</em> project. Change this file's <em>RuntimeDbChooserWindowsFormsApplication</em> or <em>RuntimeDbChooserAspNetApplication</em> class name to your <em>WxxApplication</em> descendant's name from the <em>WxxApplication.xx</em> file.<br>7. Replace the line that instantiates your WinApplication descendant in the YourSolutionName.Win/Program.xx file with the CreateApplication method call as shown in the <em>RuntimeDbChooser.Win/Program.xx file.<br></em>8. In the Application Designer invoked for the YourSolutionName.Web/WebApplication.xx file, select the Authentication Standard component and set its LogonParametersType property to RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication.</p>
<p>You can learn more on the approaches used in points 2-4 from the <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112670.aspx">eXpressApp Framework</a> > <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112682.aspx">Task-Based Help</a> > <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112982.aspx">How to: Use Custom Logon Parameters and Authentication</a> article.<br>


## Important Notes

1. In this example, the available database names are hard-coded in the `MSSqlServerChangeDatabaseHelper` class and supplied to the `DatabaseName`  property editor using the [PredefinedValues](https://documentation.devexpress.com/#eXpressAppFramework/DevExpressExpressAppModelIModelCommonMemberViewItem_PredefinedValuestopic) model option. To populate the `PredefinedValues` list with names that will be known only at runtime, implement custom property editors and assign them to the `DatabaseName` property using the [Model Editor](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112582): [How to: Supply Predefined Values for the String Property Editor Dynamically](https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument113101).
2. This `XafApplication.ConnectionString`-based implementation is designed for a simple scenario when no user and password information is stored in the connection string. Otherwise, the sensitive password information is automatically removed by XAF code from the `XafApplication.ConnectionString` and you cannot rely on this API. In such scenarios, we recommend you remember the original connection string information in the `CreateDefaultObjectSpaceProvider` method of your `XafApplication` descendant (see inside the YourSolutionName.Wxx/WxxApplication.xx file) as demonstrated in [the E2829 example](https://supportcenter.devexpress.com/ticket/details/e2829#).
