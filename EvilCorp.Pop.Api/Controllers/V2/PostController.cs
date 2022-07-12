using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvilCorp.Pop.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route(ApiRoute.BaseRoute)]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/<PostController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PostController>/5
        [HttpGet(ApiRoute.Post.RouteId)]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
