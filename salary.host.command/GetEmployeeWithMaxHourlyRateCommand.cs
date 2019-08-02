using MediatR;

namespace salary.host.command
{
    public class GetEmployeeWithMaxHourlyRateCommand : IRequest<salary.dto.Employee>
    {
        
    }
}