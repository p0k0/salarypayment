using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using salary.domain;
using salary.service;

namespace salary.host.webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        // GET api/v1/service
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/v1/service/employee/name
        [HttpGet("employee/{name}")]
        public ActionResult<EmployeeBase> GetEmployee(string name, [FromServices]ISalaryService service)
        {
            return Ok(service.Get(name));
        }
        
        [HttpGet("rate/hourly/max")]
        public ActionResult<EmployeeBase> GetEmployeeWithMaxHourlyRate([FromServices]ISalaryService service)
        {
            return Ok(service.GetEmployeeWithMaxHourlyRate());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
