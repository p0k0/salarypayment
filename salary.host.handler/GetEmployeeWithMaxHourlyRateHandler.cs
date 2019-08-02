using MediatR;
using salary.dal;
using salary.host.command;

namespace salary.host.handler
{
    public class GetEmployeeWithMaxHourlyRateHandler : RequestHandler<GetEmployeeWithMaxHourlyRateCommand, salary.dto.Employee>
    {
        private readonly IRepository _repository;

        public GetEmployeeWithMaxHourlyRateHandler(IRepository repository)
        {
            _repository = repository;
        }

        protected override salary.dto.Employee Handle(GetEmployeeWithMaxHourlyRateCommand request)
        {
            return _repository.GetEmployeeWithMaxHourlyRate();
        }
    }
}