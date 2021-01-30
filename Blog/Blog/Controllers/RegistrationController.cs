using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Domain.Registration;
using Blog.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : BlogBaseController
    {

        private readonly IRegistrationWriter _registrationWriter;

        public RegistrationController(IRegistrationWriter registrationWriter)
        {
            _registrationWriter = registrationWriter;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<Guid> Post([FromBody] RegistrationModel registrationModel,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await _registrationWriter.InsertAsync(registrationModel, cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
