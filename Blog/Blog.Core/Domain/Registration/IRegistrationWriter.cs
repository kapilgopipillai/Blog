using Blog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Domain.Registration
{
    public interface IRegistrationWriter
    {
        Task<Guid> InsertAsync(RegistrationModel registrationModel, CancellationToken cancellationToken);
    }
}
