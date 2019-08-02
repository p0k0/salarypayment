using MediatR;

namespace salary.host.command
{
    public class GetTotalSalarySumCommand : IRequest<double>
    {
    }
}