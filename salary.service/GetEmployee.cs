using salary.dal;

namespace salary.service
{
    public sealed class GetEmployee : Command
    {
        public salary.dto.Employee Employee
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
        public GetEmployee(IRepository repository, string name) : base(repository)
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