using System.Collections.Generic;
using salary.domain;

namespace salary.service
{
    public interface ISalaryService
    {
        EmployeeBase Get(string name);
        bool Save(salary.dto.Employee employee);
        IEnumerable<EmployeeBase> GetMany(long limit, long offset);
        EmployeeBase GetEmployeeWithMaxHourlyRate();
        double GetMonthlyCost();
    }
}