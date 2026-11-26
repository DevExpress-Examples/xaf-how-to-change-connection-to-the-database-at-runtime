using DevExpress.ExpressApp.Security;
using RuntimeDbChooser.Services;
using System.Linq;
using System.Security.Claims;

namespace RuntimeDbChooser.Blazor.Server.Services;

public class ConnectionStringProvider : IConnectionStringProvider
{
    public const string DatabaseNameClaimType = "RuntimeDbChooser.DatabaseName";
    readonly IPrincipalProvider principalProvider;
    readonly ISecurityStrategyBase security;
    readonly IConnectionStringHelper connectionStringHelper;

    public ConnectionStringProvider(IPrincipalProvider principalProvider, ISecurityStrategyBase security, IConnectionStringHelper connectionStringHelper)
    {
        this.principalProvider = principalProvider;
        this.security = security;
        this.connectionStringHelper = connectionStringHelper;
    }

    public string GetConnectionString()
    {
        // A signed-in user: the database chosen at logon is in the user's claims.
        string? targetDataBaseName = (principalProvider.User as ClaimsPrincipal)?.FindFirst(DatabaseNameClaimType)?.Value;
        // The logon itself: the database is in the logon parameters being entered.
        targetDataBaseName ??= (security.LogonParameters as IDatabaseNameParameter)?.DatabaseName?.Name;
        if (targetDataBaseName != null)
        {
            return connectionStringHelper.GetConnectionStringsMap()[targetDataBaseName];
        }
        return connectionStringHelper.GetConnectionStringsMap().Values.First();
    }
}

