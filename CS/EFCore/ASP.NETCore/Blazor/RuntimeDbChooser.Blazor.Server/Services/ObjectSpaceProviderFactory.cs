using System;
//using DevExpress.EntityFrameworkCore.Security;
//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Core;
//using DevExpress.ExpressApp.DC;
//using DevExpress.ExpressApp.EFCore;
//using DevExpress.ExpressApp.Security;
//using Microsoft.EntityFrameworkCore;
//using RuntimeDbChooser.Module.BusinessObjects;
//using RuntimeDbChooser.Services;
//using System.Collections.Generic;
//using System.Security.Claims;

//namespace RuntimeDbChooser.Blazor.Server.Services;
//public sealed class ObjectSpaceProviderFactory : IObjectSpaceProviderFactory {
//    readonly ISecurityStrategyBase security;
//    readonly ITypesInfo typesInfo;
//    readonly IPrincipalProvider principalProvider;
//    readonly IConnectionStringHelper connectionStringHelper;

//    readonly IServiceProvider serviceProvider;
//    public ObjectSpaceProviderFactory(IServiceProvider serviceProvider, ISecurityStrategyBase security, ITypesInfo typesInfo, IPrincipalProvider principalProvider, IConnectionStringHelper connectionStringHelper) {
//        this.security = security;
//        this.serviceProvider = serviceProvider;
//        this.typesInfo = typesInfo;
//        this.principalProvider = principalProvider;
//        this.connectionStringHelper = connectionStringHelper;
//    }

//    IEnumerable<IObjectSpaceProvider> IObjectSpaceProviderFactory.CreateObjectSpaceProviders() {
//        EFCoreObjectSpaceProvider efCoreObjectSpaceProvider = new SecuredEFCoreObjectSpaceProvider(
//            (ISelectDataSecurityProvider)security, typeof(DemoDbContext), typesInfo, GetConnectionString(),
//            (builder, connectionString) => {
//                builder.UseSqlServer(connectionString);
//            });

//        yield return efCoreObjectSpaceProvider;
//        yield return new NonPersistentObjectSpaceProvider(serviceProvider, typesInfo, null);
//    }

//    string GetConnectionString() {
//        // A signed-in user: the database chosen at logon is in the user's claims (added in OnCustomizeLoginToken).
//        string? targetDataBaseName = (principalProvider.User as ClaimsPrincipal)?.FindFirst(ConnectionStringProvider.DatabaseNameClaimType)?.Value;
//        // The logon itself: the database is in the logon parameters being entered.
//        targetDataBaseName ??= (security.LogonParameters as IDatabaseNameParameter)?.DatabaseName?.Name;
//        if(targetDataBaseName != null) {
//            return connectionStringHelper.GetConnectionStringsMap()[targetDataBaseName];
//        }
//        return "not set";
//    }
//}
