using System.Collections.Generic;
using System.Linq;
using salary.dal;

namespace salary.service.command
{
    public sealed class GetManyEmployeeCommand : Command
    {
        private readonly long _limit;
        private readonly long _offset;

        public IList<salary.dto.Employee> Result
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
        public GetManyEmployeeCommand(long limit, long offset, IRepository repository) : base(repository)
        {
            _limit = limit;
            _offset = offset;
        }
        
        public override void Execute()
        {
            base.Execute();
            _employee = _repository.GetMany(_limit, _offset).ToList();
        }

        private IList<salary.dto.Employee> _employee;
    }
}