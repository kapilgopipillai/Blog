using Blog.Common;
using Blog.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Writer.PostBlog
{
    public class PostBlogDataWriter : IPostBlogDataWriter
    {
        private readonly IDatabaseSettings _databaseSettings;
        public PostBlogDataWriter(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_databaseSettings.Tenant))
            {
                await connection.OpenAsync(cancellationToken);

                var execParams = new DynamicParameters(new { Id = id });


                var command = new CommandDefinition(
                    "dbo.DeleteBlogPost",
                    execParams,
                    commandType: System.Data.CommandType.StoredProcedure);

                await connection.ExecuteAsync(command);
            }
        }

        public async Task<Guid> InsertAsync(PostEntity postEntity, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_databaseSettings.Tenant))
            {
                await connection.OpenAsync(cancellationToken);

                var execParams = CreateParameters(postEntity);

                var command = new CommandDefinition(
                    "dbo.InsertBlogPost",
                    execParams,
                    commandType: System.Data.CommandType.StoredProcedure);

                return await connection.QuerySingleAsync<Guid>(command);
            }
        }

        public async Task UpdateAsync(PostEntity postEntity, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_databaseSettings.Tenant))
            {
                await connection.OpenAsync(cancellationToken);

                var execParams = CreateParameters(postEntity);
                execParams.AddDynamicParams(new { Id = postEntity.Id });

                var command = new CommandDefinition(
                    "dbo.UpdateBlogPost",
                    execParams,
                    commandType: System.Data.CommandType.StoredProcedure);

                await connection.ExecuteAsync(command);
            }
        }
        private static DynamicParameters CreateParameters(PostEntity postEntity) =>
            new DynamicParameters(
                new
                {
                    postEntity.authorId,
                    postEntity.title,
                    postEntity.metaTitle,
                    postEntity.summary,
                    postEntity.published,
                    postEntity.content,
                    postEntity.Disabled,
                    postEntity.Created,
                    postEntity.CreatedBy,
                    postEntity.Modified,
                    postEntity.ModifiedBy
                });
    }
}
