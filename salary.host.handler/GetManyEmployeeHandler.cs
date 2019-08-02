using System.Collections.Generic;
using System.Linq;
using MediatR;
using salary.dal;
using salary.host.command;

namespace salary.host.handler
{
    public class GetManyEmployeeHandler : RequestHandler<GetManyEmployeeCommand, IReadOnlyList<salary.dto.Employee>>
    {
        private readonly IRepository _repository;

        public GetManyEmployeeHandler(IRepository repository)
        {
            _repository = repository;
        }

        protected override IReadOnlyList<salary.dto.Employee> Handle(GetManyEmployeeCommand request)
        {
            return _repository.GetMany(request.Limit, request.Offset).ToList();
        }
    }
}