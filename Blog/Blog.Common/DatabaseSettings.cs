using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public DatabaseSettings(DatabaseSettingsConfig config)
        {
            Catalog = config.ConnectionString;
        }
        public string Catalog { get; }

        public string Tenant => GetTennantConnectionString();


        private string GetTennantConnectionString()
        {

            var connectionString = Catalog;

            return connectionString;
        }

        public string GetTenantConnectionString(string tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
