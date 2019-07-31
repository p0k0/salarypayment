using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using salary.domain;
using salary.service;

namespace salary.host.webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
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

        // POST api/v1/service/employees
        [HttpPost("employees")]
        public ActionResult<bool> Post([FromServices]ISalaryService service, webapi.dto.Employee employee)
        {
            if (double.TryParse(employee.Rate, out double rateNum) == false)
                return BadRequest();
            
            return Ok(service.Save(new salary.dto.Employee()
            {
                Name = employee.Name,
                Kind = employee.Kind,
                Rate = rateNum
            }));
        }
    }
}
