using Blog.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Writer.PostBlog
{
    public interface IPostBlogDataWriter
    {
        Task<Guid> InsertAsync(PostEntity postEntity, CancellationToken cancellationToken);

        Task UpdateAsync(PostEntity postEntity, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
