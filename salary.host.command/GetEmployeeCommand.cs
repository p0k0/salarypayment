using MediatR;

namespace salary.host.command
{
    public sealed class GetEmployeeCommand : IRequest<salary.dto.Employee>
    {
        public string Name { get; set; }
    }
}