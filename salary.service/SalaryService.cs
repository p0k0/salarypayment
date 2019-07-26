using System.Collections.Generic;
using salary.dal;
using salary.service.command;

namespace salary.service
{
    public class SalaryService : ISalaryService
    {
        private readonly IRepository _repository;

        public SalaryService(IRepository repository)
        {
            _repository = repository;
        }
        
        public salary.dto.Employee Get(string name)
        {
            var cmd = new GetEmployeeCommand(name, _repository);
            cmd.Execute();
            return cmd.Result;
        }

        public bool Save(salary.dto.Employee employee)
        {
            var cmd = new SaveEmployeeCommand(employee, _repository);
            cmd.Execute();
            return cmd.IsSuccess;
        }

        public IEnumerable<salary.dto.Employee> GetMany(int skip, int limit)
        {
            throw new System.NotImplementedException();
        }

        public salary.dto.Employee GetMostExpensiveEmployee(short kind)
        {
            throw new System.NotImplementedException();
        }

        public double GetMonthlyCost()
        {
            throw new System.NotImplementedException();
        }
    }
}