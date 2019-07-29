using System.Collections.Generic;

namespace salary.dal
{
    public interface IRepository
    {
        salary.dto.Employee Get(string name);
        bool Save(salary.dto.Employee employee);
        IEnumerable<salary.dto.Employee> GetMany(long limit, long offset);
        salary.dto.Employee GetEmployeeWithMaxHourlySalary();
        double GetMonthlyCost();
    }
}