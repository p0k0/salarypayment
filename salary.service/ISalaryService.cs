using System.Collections.Generic;
using salary.domain;

namespace salary.service
{
    public interface ISalaryService
    {
        EmployeeBase Get(string name);
        bool Save(EmployeeBase employee);
        IEnumerable<EmployeeBase> GetMany(long limit, long offset);
        EmployeeBase GetEmployeeWithMaxHourlySalary();
        double GetMonthlyCost();
    }
}