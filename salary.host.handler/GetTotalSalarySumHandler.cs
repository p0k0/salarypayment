using MediatR;
using salary.dal;
using salary.host.command;

namespace salary.host.handler
{
    public class GetTotalSalarySumHandler : RequestHandler<GetTotalSalarySumCommand, double>
    {
        private readonly IRepository _repository;

        public GetTotalSalarySumHandler(IRepository repository)
        {
            _repository = repository;
        }

        protected override double Handle(GetTotalSalarySumCommand request)
        {
            return _repository.GetMonthlyCost();
        }
    }
}