using DevExpress.ExpressApp.Security;
using Microsoft.Extensions.Configuration;
using RuntimeDbChooser.Module.BusinessObjects;
using RuntimeDbChooser.Services;

namespace RuntimeDbChooser.Blazor.Server.Services;
public class ConnectionStringProvider : IConnectionStringProvider {
    readonly IConfiguration configuration;
    readonly ILogonParameterProvider logonParameterProvider;

    public ConnectionStringProvider(IConfiguration configuration, ILogonParameterProvider logonParameterProvider) {
        this.configuration = configuration;
        this.logonParameterProvider = logonParameterProvider;
    }

    public string GetConnectionString() {
        string? connectionString = null;
        if(configuration.GetConnectionString("ConnectionString") != null) {
            connectionString = configuration.GetConnectionString("ConnectionString");
        }
#if EASYTEST
        if(configuration.GetConnectionString("EasyTestConnectionString") != null) {
            connectionString = configuration.GetConnectionString("EasyTestConnectionString");
        }
#endif
        //Configure the connection string based on logon parameter values.
        string targetDataBaseName = logonParameterProvider.GetLogonParameters<IDatabaseNameParameter>().DatabaseName;
        var result = MSSqlServerChangeDatabaseHelper.PatchConnectionString(targetDataBaseName, connectionString);
        return result;
    }
}

