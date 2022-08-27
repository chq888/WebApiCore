using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAppApi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }

    /// <summary>
    /// ValuesController
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [MapToApiVersion("1.0")]
        [Route("", Name = "Get")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[MapToApiVersion("1.1")]
        //[Route("", Name = "Get2")]
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get(bool hasNew)
        //{
        //    return new string[] { "Get2", "Get2" };
        //}

        /// <summary>
        /// get item by id
        /// </summary>
        /// <param name="id">id of item you want to get</param>
        /// <returns>ActionResult of type IEnumerable</returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
