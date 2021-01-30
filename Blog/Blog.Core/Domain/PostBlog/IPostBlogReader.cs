using Blog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Domain.PostBlog
{
    public interface IPostBlogReader
    {
        Task<PostModel> ReadAsync(Guid Id, CancellationToken cancellationToken);
        Task<List<PostModel>> ReadAllAsync(CancellationToken cancellationToken);
    }
}
