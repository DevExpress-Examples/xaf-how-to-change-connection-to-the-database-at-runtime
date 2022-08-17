﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RuntimeDbChooser.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RuntimeDbChooser.Module.BusinessObjects;

[DomainComponent]
public class CustomLogonParametersForStandardAuthentication : AuthenticationStandardLogonParameters, IDatabaseNameParameter, IServiceProviderConsumer {
    IServiceProvider serviceProvider;
    private DataBaseNameHolder dataBaseNameObj;

    [DataSourceProperty(nameof(GetDataBaseNames), DataSourcePropertyIsNullMode.SelectAll)]
    public DataBaseNameHolder DatabaseName {
        get { return dataBaseNameObj; }
        set {
            dataBaseNameObj = value;
        }
    }

    IReadOnlyList<DataBaseNameHolder>? dataBaseNameObjs = null;
    [Browsable(false)]
    [JsonIgnore]
    public IReadOnlyList<DataBaseNameHolder> GetDataBaseNames {
        get {
            if(dataBaseNameObjs == null) {
                dataBaseNameObjs = new List<DataBaseNameHolder>();
                IConnectionStringHelper connectionStringHelper = serviceProvider.GetRequiredService<IConnectionStringHelper>();
                foreach(var dbname in connectionStringHelper.GetConnectionStringsMap().Keys) {
                    ((List<DataBaseNameHolder>)dataBaseNameObjs).Add(new DataBaseNameHolder(dbname));
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
//set readonly using model editor
//<DetailView Id = "DataBaseNameHolder_DetailView" AllowEdit="False" />
//CustomLogonController checks that the DataBaseNameHolder property is not null
public class DataBaseNameHolder : NonPersistentLiteObject {
    public DataBaseNameHolder(string name) {
        Name = name;
    }
    public string Name { get; }
}

