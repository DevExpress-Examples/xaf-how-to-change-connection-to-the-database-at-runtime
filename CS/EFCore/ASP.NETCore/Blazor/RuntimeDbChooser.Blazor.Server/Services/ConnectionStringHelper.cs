using Microsoft.Extensions.Configuration;
using RuntimeDbChooser.Services;
using System.Collections.Generic;

namespace RuntimeDbChooser.Blazor.Server.Services;
public class ConnectionStringHelper : IConnectionStringHelper {
    readonly IConfiguration configuration;

    public ConnectionStringHelper(IConfiguration configuration) {
        this.configuration = configuration;
    }
    public IDictionary<string, string> GetConnectionStringsMap() {
        Dictionary<string, string> connectionStrings = new Dictionary<string, string>();
        var connectionsStr = configuration.GetSection("ConnectionStrings");
        foreach(var conf in connectionsStr.GetChildren()) {
            connectionStrings.Add(conf.Key, conf.Value);
        }
        return connectionStrings;
    }
}

