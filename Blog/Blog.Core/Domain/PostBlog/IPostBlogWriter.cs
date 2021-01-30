using Blog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Domain.PostBlog
{
    public interface IPostBlogWriter
    {
        Task<Guid> InsertAsync(PostModel postModel, CancellationToken cancellationToken);

        Task UpdateAsync(PostModel postModel, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
