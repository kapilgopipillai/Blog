using Blog.Common;
using Blog.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Reader.PostBlog
{
    public class PostBlogDataReader : IPostBlogDataReader
    {
        private readonly IDatabaseSettings _databaseSettings;
        public PostBlogDataReader(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }
        public async Task<List<PostEntity>> ReadAllAsync(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_databaseSettings.Tenant))
            {
                await connection.OpenAsync(cancellationToken);
                var command = new CommandDefinition(
                    "dbo.GetAllBlogPost",
                    commandType: System.Data.CommandType.StoredProcedure);

                var data = await connection.QueryAsync<PostEntity>(command);
                var totalCount = data.Count();
                var records = data.ToList();
                return records;
            }
        }

        public async Task<PostEntity> ReadAsync(Guid Id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_databaseSettings.Tenant))
            {
                await connection.OpenAsync(cancellationToken);
                var execParams = new DynamicParameters(new { Id });
                var command = new CommandDefinition(
                    "dbo.GetBlogPost",
                    execParams,
                    commandType: System.Data.CommandType.StoredProcedure);
                var data = await connection.QuerySingleOrDefaultAsync<PostEntity>(command);
                return data;
            }
        }
    }
}
