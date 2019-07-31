using salary.dal;

namespace salary.service.command
{
    public class GetEmployeeWithMaxHourlyRateCommand : Command
    {
        public GetEmployeeWithMaxHourlyRateCommand(IRepository repository) : base(repository)
        {
        }

        public salary.dto.Employee Result
        {
            get
            {
                if (_employee == null)
                {
                    Execute();
                }

                return _employee;
            }
        }

        public override void Execute()
        {
            base.Execute();
            _employee = _repository.GetEmployeeWithMaxHourlyRate();
        }

        private salary.dto.Employee _employee;
    }
}