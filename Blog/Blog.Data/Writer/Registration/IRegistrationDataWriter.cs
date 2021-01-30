using Blog.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Writer.Registration
{
    public interface IRegistrationDataWriter
    {
        Task<Guid> InsertAsync(RegistrationEnity registrationEnity, CancellationToken cancellationToken);
    }
}
