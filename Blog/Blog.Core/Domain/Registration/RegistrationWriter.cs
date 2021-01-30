using Blog.Core.Mapping;
using Blog.Data.Writer.Registration;
using Blog.Entity;
using Blog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Domain.Registration
{
    public class RegistrationWriter: IRegistrationWriter
    {
        private readonly IRegistrationDataWriter _registrationDataWriter;
        private readonly IObjectMapper _objectMapper;
        public RegistrationWriter(IRegistrationDataWriter registrationDataWriter,
            IObjectMapper objectMapper)
        {
            _registrationDataWriter = registrationDataWriter;
            _objectMapper = objectMapper;
        }

        public async Task<Guid> InsertAsync(RegistrationModel registrationModel, CancellationToken cancellationToken)
        {
            registrationModel.Created = DateTime.UtcNow;
            registrationModel.Modified = DateTime.UtcNow;
            var RegistrationModel = _objectMapper.Map<RegistrationModel, RegistrationEnity>(registrationModel);
            return await _registrationDataWriter.InsertAsync(RegistrationModel, cancellationToken);
        }
    }
}
