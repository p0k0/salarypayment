using System.Collections.Generic;
using MediatR;

namespace salary.host.command
{
    public sealed class GetManyEmployeeCommand : IRequest<IReadOnlyList<salary.dto.Employee>>
    {
        public long Limit { get; set; }
        public long Offset { get; set; }
    }
}