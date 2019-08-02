using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using salary.domain;
using salary.service;

namespace salary.host.webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private IMapper _mapper;

        public ServiceController()
        {
            //webapi.dto
            var config = new MapperConfiguration(_ =>
            {
                _.CreateMap<EmployeeBase, webapi.dto.Employee>();
            });
            _mapper = new Mapper(config);
        }
        
        [ResponseCache(Duration = 10)]
        // GET api/v1/service/employees/name
        [HttpGet("employees/{name}")]
        public async Task<ActionResult<webapi.dto.Employee>> GetEmployee([FromServices]ISalaryService service, string name)
        {
            var employee = _mapper.Map<EmployeeBase, webapi.dto.Employee>(service.Get(name));
            return await Task.FromResult<ActionResult<webapi.dto.Employee>>(Ok(employee));
        }
        
        [ResponseCache(Duration = 10)]
        // GET api/v1/service/employees
        [HttpGet("employees")]
        public ActionResult<IEnumerable<webapi.dto.Employee>> GetEmployees([FromServices]ISalaryService service, [FromQuery]long limit = 2, [FromQuery]long offset = 0)
        {
            var employees = _mapper.Map<IEnumerable<EmployeeBase>, IReadOnlyList<webapi.dto.Employee>>(service.GetMany(limit, offset));
            return Ok(employees);
        }
        
        [ResponseCache(Duration = 10)]
        //GET api/v1/service/rate/hourly/max
        [HttpGet("rate/hourly/max")]
        public ActionResult<webapi.dto.Employee> GetEmployeeWithMaxHourlyRate([FromServices]ISalaryService service)
        {
            var employee = _mapper.Map<EmployeeBase, webapi.dto.Employee>(service.GetEmployeeWithMaxHourlyRate());
            return Ok(employee);
        }
        
        [ResponseCache(Duration = 10)]
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
