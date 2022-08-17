using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RuntimeDbChooser.Module.NetStandard;
public class ConnectionStringHelper {
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
