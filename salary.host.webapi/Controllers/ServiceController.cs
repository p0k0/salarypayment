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

        // GET api/v1/service/employees/name
        [HttpGet("employees/{name}")]
        public ActionResult<EmployeeBase> GetEmployee([FromServices]ISalaryService service, string name)
        {
            return Ok(service.Get(name));
        }
        
        // GET api/v1/service/employees
        [HttpGet("employees")]
        public ActionResult<IEnumerable<EmployeeBase>> GetEmployees([FromServices]ISalaryService service, [FromQuery]long limit = 2, [FromQuery]long offset = 0)
        {
            return Ok(service.GetMany(limit: limit, offset: offset));
        }
        
        //GET api/v1/service/rate/hourly/max
        [HttpGet("rate/hourly/max")]
        public ActionResult<EmployeeBase> GetEmployeeWithMaxHourlyRate([FromServices]ISalaryService service)
        {
            return Ok(service.GetEmployeeWithMaxHourlyRate());
        }
        
        //GET api/v1/service/salary/sum
        [HttpGet("salary/sum")]
        public ActionResult<double> GetTotalSalarySum([FromServices]ISalaryService service)
        {
            return Ok(service.GetMonthlyCost());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
