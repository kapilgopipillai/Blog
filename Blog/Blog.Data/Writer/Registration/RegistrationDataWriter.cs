using Blog.Common;
using Blog.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Writer.Registration
{
    public class RegistrationDataWriter: IRegistrationDataWriter
    {
        private readonly IDatabaseSettings _databaseSettings;
        public RegistrationDataWriter(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<Guid> InsertAsync(RegistrationEnity registrationEnity, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_databaseSettings.Tenant))
            {
                await connection.OpenAsync(cancellationToken);

                var execParams = CreateParameters(registrationEnity);

                var command = new CommandDefinition(
                    "dbo.InsertRegistration",
                    execParams,
                    commandType: System.Data.CommandType.StoredProcedure);

                return await connection.QuerySingleAsync<Guid>(command);
            }
        }

        private static DynamicParameters CreateParameters(RegistrationEnity registrationEnity) =>
            new DynamicParameters(
                new
                {
                    registrationEnity.Name,
                    registrationEnity.UserName,
                    registrationEnity.UserPassword,
                    registrationEnity.EmailAddress,
                    registrationEnity.PhoneNumber,
                    registrationEnity.Address,
                    registrationEnity.City,
                    registrationEnity.State,
                    registrationEnity.PostalCode,
                    registrationEnity.Disabled,
                    registrationEnity.Created,
                    registrationEnity.CreatedBy,
                    registrationEnity.Modified,
                    registrationEnity.ModifiedBy
                });
    }
}
