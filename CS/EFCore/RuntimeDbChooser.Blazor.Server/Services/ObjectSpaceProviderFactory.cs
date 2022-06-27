using System.Collections.Generic;
using DevExpress.EntityFrameworkCore.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Blazor.Server.Services;
public sealed class ObjectSpaceProviderFactory : IObjectSpaceProviderFactory {
    readonly ISecurityStrategyBase security;
    readonly ITypesInfo typesInfo;
    readonly IConfiguration configuration;
    readonly ILogonParameterProvider logonParameterProvider;

    public ObjectSpaceProviderFactory(ISecurityStrategyBase security, ITypesInfo typesInfo, IConfiguration configuration, ILogonParameterProvider logonParameterProvider) {
        this.security = security;
        this.typesInfo = typesInfo;
        this.configuration = configuration;
        this.logonParameterProvider = logonParameterProvider;
    }

    IEnumerable<IObjectSpaceProvider> IObjectSpaceProviderFactory.CreateObjectSpaceProviders() {
        EFCoreObjectSpaceProvider efCoreObjectSpaceProvider = new SecuredEFCoreObjectSpaceProvider(
            (ISelectDataSecurityProvider)security, typeof(DemoDbContext), typesInfo, GetConnectionString(),
            (builder, connectionString) => {
                builder.UseSqlServer(connectionString);
            });

        yield return efCoreObjectSpaceProvider;
        yield return new NonPersistentObjectSpaceProvider(typesInfo, null);
    }

    string GetConnectionString() {
        string? connectionString = null;
        if(configuration.GetConnectionString("ConnectionString") != null) {
            connectionString = configuration.GetConnectionString("ConnectionString");
        }
#if EASYTEST
        if(configuration.GetConnectionString("EasyTestConnectionString") != null) {
            connectionString = configuration.GetConnectionString("EasyTestConnectionString");
        }
#endif
        string targetDataBaseName = logonParameterProvider.GetLogonParameters<IDatabaseNameParameter>().DatabaseName;

        var result = MSSqlServerChangeDatabaseHelper.PatchConnectionString(targetDataBaseName, connectionString);
        return result;
    }
}
