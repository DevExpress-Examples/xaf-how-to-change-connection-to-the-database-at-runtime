using DevExpress.ExpressApp.Security;
using RuntimeDbChooser.Services;

namespace RuntimeDbChooser.Blazor.Server.Services;
public class ConnectionStringProvider : IConnectionStringProvider {
    readonly ILogonParameterProvider logonParameterProvider;
    readonly IConnectionStringHelper connectionStringHelper;

    public ConnectionStringProvider(ILogonParameterProvider logonParameterProvider, IConnectionStringHelper connectionStringHelper) {
        this.logonParameterProvider = logonParameterProvider;
        this.connectionStringHelper = connectionStringHelper;
    }

    public string GetConnectionString() {
        //Configure the connection string based on logon parameter values.
        string? targetDataBaseName = logonParameterProvider.GetLogonParameters<IDatabaseNameParameter>().DatabaseName?.Name;
        if(targetDataBaseName != null) {
            return connectionStringHelper.GetConnectionStringsMap()[targetDataBaseName];
        }
        return "not set";
    }
}

