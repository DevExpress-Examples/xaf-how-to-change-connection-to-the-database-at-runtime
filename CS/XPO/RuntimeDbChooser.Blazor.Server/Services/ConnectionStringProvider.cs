using DevExpress.ExpressApp.Security;
using Microsoft.Extensions.Configuration;
using RuntimeDbChooser.Module.BusinessObjects;
using RuntimeDbChooser.Module.NetStandard;
using System.Linq;

namespace RuntimeDbChooser.Blazor.Server.Services;
public class ConnectionStringProvider {
    readonly IConfiguration configuration;
    readonly ILogonParameterProvider logonParameterProvider;
    readonly ConnectionStringHelper connectionStringHelper;

    public ConnectionStringProvider(IConfiguration configuration, ILogonParameterProvider logonParameterProvider, ConnectionStringHelper connectionStringHelper) {
        this.configuration = configuration;
        this.logonParameterProvider = logonParameterProvider;
        this.connectionStringHelper = connectionStringHelper;
    }

    public string GetConnectionString() {
        //Configure the connection string based on logon parameter values.
        string? targetDataBaseName = logonParameterProvider.GetLogonParameters<IDatabaseNameParameter>().DatabaseName?.Name;
        if(targetDataBaseName != null) {
            return connectionStringHelper.GetConnectionStringsMap()[targetDataBaseName];
        }
        return connectionStringHelper.GetConnectionStringsMap().Values.First();
    }
}

