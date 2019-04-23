using EchoServer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.Serialization;

namespace EchoServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {

        private ReturnMessage _result;
        // GET api/echo
        [HttpGet]
        public ObjectResult Get()
        {
            if (_result == null)
                _result = new ReturnMessage(this.HttpContext.Request, "");

            Console.WriteLine(_result.ToString());
            return new ObjectResult(_result);
        }

        // POST api/echo
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _result = new ReturnMessage(this.HttpContext.Request, "");
            RedirectToRoute("api/echo");
        }

        // PUT api/echo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _result = new ReturnMessage(this.HttpContext.Request, "");
            RedirectToRoute("api/echo");
        }

        // DELETE api/echo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _result = new ReturnMessage(this.HttpContext.Request, "");
            RedirectToRoute("api/echo");
        }
    }
}