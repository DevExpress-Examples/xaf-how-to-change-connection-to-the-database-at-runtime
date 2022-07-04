using System.Collections.Generic;

namespace RuntimeDbChooser.Services;
public interface IConnectionStringHelper {
    IDictionary<string, string> GetConnectionStringsMap();
}
