using salary.dal;

namespace salary.service.command
{
    public sealed class GetEmployeeCommand : Command
    {
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
        public GetEmployeeCommand(string name, IRepository repository) : base(repository)
        {
            _name = name;
        }
        
        public override void Execute()
        {
            _employee = _repository.Get(_name);
        }

        private readonly string _name;
        private salary.dto.Employee _employee;
    }
}