using System;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetReachHourlyEmployee : DbCommand
    {
        public GetReachHourlyEmployee(MySqlConnection connection) : base(connection)
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