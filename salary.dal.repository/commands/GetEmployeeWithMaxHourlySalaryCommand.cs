using System;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetEmployeeWithMaxHourlySalaryCommand : DbCommand
    {
        private dto.Employee _employee;
        public dto.Employee Result
        {
            get
            {
                if (_employee == null)
                {
                    Execute();
                }

                return _employee;
            }
        }
        public GetEmployeeWithMaxHourlySalaryCommand(MySqlConnection connection) : base(connection)
        {
        }

        public override void Execute()
        {
            /*
            SELECT e.id, e.name, s.kind, max(s.rate)
            FROM salary s, employee e
            WHERE e.id = s.employee_id and s.kind = 'hourly';*/
            throw new NotImplementedException();
        }
    }
}