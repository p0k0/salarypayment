using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using salary.dal;
using salary.domain;
using salary.host;
using salary.service.command;

namespace salary.service
{
    public sealed class SalaryService : ISalaryService
    {
        private readonly IMediator _mediator;
        private readonly IRepository _repository;
        private IMapper _mapper;

        public SalaryService(IMediator mediator, IMapConfigureFactory mapConfigureFactory)
        {
            _mediator = mediator;
            _mapper = mapConfigureFactory.CreateServiceMap();
            
        }
        
        public EmployeeBase Get(string name)
        {
            var cmd = new GetEmployeeCommand(name, _repository);
            cmd.Execute();
            return _mapper.Map<salary.dto.Employee, EmployeeBase>(cmd.Result);
        }

        public bool Save(salary.dto.Employee employee)
        {
            var cmd = new SaveEmployeeCommand(employee, _repository);
            cmd.Execute();
            return cmd.IsSuccess;
        }

        public IEnumerable<EmployeeBase> GetMany(long limit, long offset)
        {
            var cmd = new GetManyEmployeeCommand(limit, offset, _repository);
            cmd.Execute();
            return cmd.Result.Select(_ => _mapper.Map<salary.dto.Employee, EmployeeBase>(_));
        }

        public EmployeeBase GetEmployeeWithMaxHourlyRate()
        {
            var cmd = new GetEmployeeWithMaxHourlyRateCommand(_repository);
            cmd.Execute();
            return _mapper.Map<salary.dto.Employee, EmployeeBase>(cmd.Result);
        }

        public double GetMonthlyCost()
        {
            var cmd = new GetTotalSalarySumCommand(_repository);
            return cmd.Result;
        }
    }
}