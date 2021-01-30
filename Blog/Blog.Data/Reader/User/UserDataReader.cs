using Blog.Common;
using Blog.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Reader.User
{
    public class UserDataReader : IUserDataReader
    {
        private readonly IDatabaseSettings _databaseSettings;
        public UserDataReader(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<RegistrationEnity> Login(string UserName, string Password)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseSettings.Tenant))
                {
                    await connection.OpenAsync();

                    var execParams = new DynamicParameters(new { UserName, Password });
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@@UserName", UserName);
                    param.Add("@Password", Password);

                    var command = new CommandDefinition(
                        "dbo.UserLogin",
                        param,
                        commandType: System.Data.CommandType.StoredProcedure);

                    return await connection.QuerySingleOrDefaultAsync<RegistrationEnity>(command);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
