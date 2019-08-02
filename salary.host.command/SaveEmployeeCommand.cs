using MediatR;

namespace salary.host.command
{
    public sealed class SaveEmployeeCommand : IRequest<bool>
    {
        public salary.dto.Employee Employee { get; set; }
    }
}