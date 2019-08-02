using MediatR;
using salary.dal;
using salary.host.command;

namespace salary.host.handler
{
    public class GetEmployeeHandler : RequestHandler<GetEmployeeCommand, salary.dto.Employee>
    {
        private readonly IRepository _repository;

        public GetEmployeeHandler(IRepository repository)
        {
            _repository = repository;
        }
        
        protected override salary.dto.Employee Handle(GetEmployeeCommand request)
        {
            return _repository.Get(request.Name);
        }
    }
}