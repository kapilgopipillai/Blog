using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Domain.PostBlog;
using Blog.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostController : BlogBaseController
    {
        private readonly IPostBlogWriter _postBlogWriter;
        private readonly IPostBlogReader _postBlogReader;

        public BlogPostController(IPostBlogWriter postBlogWriter, IPostBlogReader postBlogReader)
        {
            _postBlogWriter = postBlogWriter;
            _postBlogReader = postBlogReader;
        }


        [HttpPost]  
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<Guid> Post([FromBody] PostModel postModel,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await _postBlogWriter.InsertAsync(postModel, cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] PostModel postModel,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _postBlogWriter.UpdateAsync(postModel, cancellationToken);
                return Updated();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _postBlogWriter.DeleteAsync(id, cancellationToken);
                return Deleted();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ListQueryResult<PostModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var qresult = await _postBlogReader.ReadAllAsync(cancellationToken);
                return Ok(qresult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PostModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var employee = await _postBlogReader.ReadAsync(id, cancellationToken);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
