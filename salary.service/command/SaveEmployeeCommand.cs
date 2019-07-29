using salary.dal;

namespace salary.service.command
{
    public sealed class SaveEmployeeCommand : Command
    {
        private readonly salary.dto.Employee _employee;

        public bool IsSuccess;

        public SaveEmployeeCommand(salary.dto.Employee employee, IRepository repository) : base(repository)
        {
            _employee = employee;
        }
        
        public override void Execute()
        {
            base.Execute();
            IsSuccess = _repository.Save(_employee);
        }
    }
}