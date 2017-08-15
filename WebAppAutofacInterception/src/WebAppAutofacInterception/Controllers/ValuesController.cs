using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAutofacInterception.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ISampleBusiness _sampleBusiness;

        public ValuesController(ISampleBusiness sampleBusiness)
        {
            _sampleBusiness = sampleBusiness;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var v1 = _sampleBusiness.Method2("value", "1");
            var v2 = _sampleBusiness.Method2("value", "2");

            var v3 = _sampleBusiness.Method1(1, 2);

            return new[] { v1, v2 , v3.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
