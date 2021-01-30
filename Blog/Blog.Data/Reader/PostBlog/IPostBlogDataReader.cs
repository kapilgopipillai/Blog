using Blog.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Reader.PostBlog
{
    public interface IPostBlogDataReader
    {
        Task<PostEntity> ReadAsync(Guid Id, CancellationToken cancellationToken);
        Task<List<PostEntity>> ReadAllAsync(CancellationToken cancellationToken);
    }
}
