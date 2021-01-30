using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common
{
    public interface IDatabaseSettings
    {

        string Tenant { get; }

        string GetTenantConnectionString(string tenantId);
    }
}
