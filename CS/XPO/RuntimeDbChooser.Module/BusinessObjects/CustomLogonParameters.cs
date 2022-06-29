using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RuntimeDbChooser.Module.NetStandard;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RuntimeDbChooser.Module.BusinessObjects {
    public interface IDatabaseNameParameter {
        DataBaseNameObj DatabaseName { get; set; }
    }
    [DomainComponent]
    public class CustomLogonParametersForStandardAuthentication : AuthenticationStandardLogonParameters, IDatabaseNameParameter, IServiceProviderConsumer {
        IServiceProvider serviceProvider;
        private DataBaseNameObj dataBaseNameObj;
        [DataSourceProperty(nameof(GetDataBaseNames), DataSourcePropertyIsNullMode.SelectAll)]
        public DataBaseNameObj DatabaseName {
            get { return dataBaseNameObj; }
            set {
                dataBaseNameObj = value;
            }
        }

        IReadOnlyList<DataBaseNameObj>? dataBaseNameObjs = null;
        [Browsable(false)]
        [JsonIgnore]
        public IReadOnlyList<DataBaseNameObj> GetDataBaseNames {
            get {
                if(dataBaseNameObjs == null) {
                    dataBaseNameObjs = new List<DataBaseNameObj>();
                    ConnectionStringHelper connectionStringHelper = serviceProvider.GetRequiredService<ConnectionStringHelper>();
                    foreach(var dbname in connectionStringHelper.GetConnectionStringsMap().Keys) {
                        ((List<DataBaseNameObj>)dataBaseNameObjs).Add(new DataBaseNameObj(dbname));
                    }
                }
                return dataBaseNameObjs;
            }
        }

        public void SetServiceProvider(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;
        }
    }
    [DomainComponent]
    public class DataBaseNameObj : NonPersistentLiteObject {
        public DataBaseNameObj(string name) {
            Name = name;
        }
        public string Name { get; set; }
    }
    //[DomainComponent]
    //public class CustomLogonParametersForActiveDirectoryAuthentication : IDatabaseNameParameter {
    //    private string databaseName = MSSqlServerChangeDatabaseHelper.DefaultDatabaseName;

    //    [ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)]
    //    public string DatabaseName {
    //        get { return databaseName; }
    //        set { databaseName = value; }
    //    }
    //}
}
