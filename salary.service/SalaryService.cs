using System.Collections.Generic;
using AutoMapper;
using MediatR;
using salary.domain;
using salary.host;
using salary.host.command;

namespace salary.service
{
    public sealed class SalaryService : ISalaryService
    {
        private readonly IMediator _mediator;
        private IMapper _mapper;

        public SalaryService(IMediator mediator, IMapConfigureFactory mapConfigureFactory)
        {
            _mediator = mediator;
            _mapper = mapConfigureFactory.CreateServiceMap();
        }
        
        public EmployeeBase Get(string name)
        {
            var task = _mediator.Send(new GetEmployeeCommand() { Name = name })
                .ConfigureAwait(false);
            var result = task.GetAwaiter().GetResult();
            return _mapper.Map<salary.dto.Employee, EmployeeBase>(result);
        }
        
        public EmployeeBase GetEmployeeWithMaxHourlyRate()
        {
            var task = _mediator.Send(new GetEmployeeWithMaxHourlyRateCommand())
                .ConfigureAwait(false);
            var result = task.GetAwaiter().GetResult();
            return _mapper.Map<salary.dto.Employee, EmployeeBase>(result);
        }

        public IReadOnlyList<EmployeeBase> GetMany(long limit, long offset)
        {
            var task = _mediator.Send(new GetManyEmployeeCommand() { Limit = limit, Offset = offset })
                .ConfigureAwait(false);
            var result = task.GetAwaiter().GetResult();
            return _mapper.Map<IReadOnlyList<salary.dto.Employee>, IReadOnlyList<EmployeeBase>>(result);
        }
        
        public double GetMonthlyCost()
        {
            var task = _mediator.Send(new GetTotalSalarySumCommand()).ConfigureAwait(false);
            var result = task.GetAwaiter().GetResult();
            return result;
        }

        public bool Save(salary.dto.Employee employee)
        {
            var task = _mediator.Send(new SaveEmployeeCommand() {Employee = employee})
                .ConfigureAwait(false);
            var result = task.GetAwaiter().GetResult();
            return result;
        }

    }
}