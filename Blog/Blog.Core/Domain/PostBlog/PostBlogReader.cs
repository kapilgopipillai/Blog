using Blog.Core.Mapping;
using Blog.Data.Reader.PostBlog;
using Blog.Entity;
using Blog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Domain.PostBlog
{
    public class PostBlogReader : IPostBlogReader
    {
        private readonly IPostBlogDataReader _postBlogDataReader;
        private readonly IObjectMapper _objectMapper;
        public PostBlogReader(IPostBlogDataReader postBlogDataReader,
            IObjectMapper objectMapper)
        {
            _postBlogDataReader = postBlogDataReader;
            _objectMapper = objectMapper;
        }
        public async Task<List<PostModel>> ReadAllAsync(CancellationToken cancellationToken)
        {
            var model = await _postBlogDataReader.ReadAllAsync(cancellationToken);
            var entry = _objectMapper.Map<List<PostEntity>, List<PostModel>>(model);
            return entry;
        }

        public async Task<PostModel> ReadAsync(Guid Id, CancellationToken cancellationToken)
        {
            var model = await _postBlogDataReader.ReadAsync(Id, cancellationToken);
            var entry = _objectMapper.Map<PostEntity, PostModel>(model);
            return entry;
        }
    }
}
