'using DevExpress.EntityFrameworkCore.Security;
'using DevExpress.ExpressApp;
'using DevExpress.ExpressApp.Core;
'using DevExpress.ExpressApp.DC;
'using DevExpress.ExpressApp.EFCore;
'using DevExpress.ExpressApp.Security;
'using Microsoft.EntityFrameworkCore;
'using RuntimeDbChooser.Module.BusinessObjects;
'using RuntimeDbChooser.Services;
'using System.Collections.Generic;
'namespace RuntimeDbChooser.Blazor.Server.Services;
'public sealed class ObjectSpaceProviderFactory : IObjectSpaceProviderFactory {
'    readonly ISecurityStrategyBase security;
'    readonly ITypesInfo typesInfo;
'    readonly ILogonParameterProvider logonParameterProvider;
'    readonly IConnectionStringHelper connectionStringHelper;
'    public ObjectSpaceProviderFactory(ISecurityStrategyBase security, ITypesInfo typesInfo, ILogonParameterProvider logonParameterProvider, IConnectionStringHelper connectionStringHelper) {
'        this.security = security;
'        this.typesInfo = typesInfo;
'        this.logonParameterProvider = logonParameterProvider;
'        this.connectionStringHelper = connectionStringHelper;
'    }
'    IEnumerable<IObjectSpaceProvider> IObjectSpaceProviderFactory.CreateObjectSpaceProviders() {
'        EFCoreObjectSpaceProvider efCoreObjectSpaceProvider = new SecuredEFCoreObjectSpaceProvider(
'            (ISelectDataSecurityProvider)security, typeof(DemoDbContext), typesInfo, GetConnectionString(),
'            (builder, connectionString) => {
'                builder.UseSqlServer(connectionString);
'            });
'        yield return efCoreObjectSpaceProvider;
'        yield return new NonPersistentObjectSpaceProvider(typesInfo, null);
'    }
'    string GetConnectionString() {
'        //Configure the connection string based on logon parameter values.
'        string? targetDataBaseName = logonParameterProvider.GetLogonParameters<IDatabaseNameParameter>().DatabaseName?.Name;
'        if(targetDataBaseName != null) {
'            return connectionStringHelper.GetConnectionStringsMap()[targetDataBaseName];
'        }
'        return "not set";
'    }
'}
