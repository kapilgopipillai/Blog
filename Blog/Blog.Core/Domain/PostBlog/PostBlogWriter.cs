using Blog.Core.Mapping;
using Blog.Data.Writer.PostBlog;
using Blog.Entity;
using Blog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Domain.PostBlog
{
    public class PostBlogWriter : IPostBlogWriter
    {
        private readonly IPostBlogDataWriter _postBlogDataWriter;
        private readonly IObjectMapper _objectMapper;
        public PostBlogWriter(IPostBlogDataWriter postBlogDataWriter,
            IObjectMapper objectMapper)
        {
            _postBlogDataWriter = postBlogDataWriter;
            _objectMapper = objectMapper;
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _postBlogDataWriter.DeleteAsync(id, cancellationToken);
        }

        public async Task<Guid> InsertAsync(PostModel postModel, CancellationToken cancellationToken)
        {
            postModel.Created = DateTime.UtcNow;
            postModel.Modified = DateTime.UtcNow;
            var EmployeeModel = _objectMapper.Map<PostModel, PostEntity>(postModel);
            return await _postBlogDataWriter.InsertAsync(EmployeeModel, cancellationToken);
        }

        public async Task UpdateAsync(PostModel postModel, CancellationToken cancellationToken)
        {
            postModel.Modified = DateTime.UtcNow;
            var PostModel = _objectMapper.Map<PostModel, PostEntity>(postModel);
            await _postBlogDataWriter.UpdateAsync(PostModel, cancellationToken);
        }
    }
}
