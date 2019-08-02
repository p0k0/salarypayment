using System.Collections.Generic;
using System.Threading.Tasks;
using salary.domain;

namespace salary.service
{
    public interface ISalaryService
    {
        EmployeeBase Get(string name);
        bool Save(salary.dto.Employee employee);
        IReadOnlyList<EmployeeBase> GetMany(long limit, long offset);
        EmployeeBase GetEmployeeWithMaxHourlyRate();
        double GetMonthlyCost();
    }
}