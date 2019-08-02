using MediatR;
using salary.dal;
using salary.host.command;

namespace salary.host.handler
{
    public class SaveEmployeeHandler : RequestHandler<SaveEmployeeCommand, bool>
    {
        private readonly IRepository _repository;

        public SaveEmployeeHandler(IRepository repository)
        {
            _repository = repository;
        }

        protected override bool Handle(SaveEmployeeCommand request)
        {
            return _repository.Save(request.Employee);
        }
    }
}