using salary.dal;

namespace salary.service.command
{
    public class GetEmployeeWithMaxHourlySalaryCommand : Command
    {
        public GetEmployeeWithMaxHourlySalaryCommand(IRepository repository) : base(repository)
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
            _employee = _repository.GetEmployeeWithMaxHourlySalary();
        }

        private salary.dto.Employee _employee;
    }
}