using System;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetTotalSalarySumCommand : DbCommand
    {
        private double _salarySum;
        public bool IsCalculated { get; private set; }

        public double Result
        {
            get
            {
                if (!IsCalculated)
                {
                    Execute();
                }

                return _salarySum;
            }
        }

        public GetTotalSalarySumCommand(MySqlConnection connection) : base(connection)
        {
        }

        public override void Execute()
        {
            /*
             SELECT sum(if (s.kind like 'monthly', s.rate, s.rate * 8.0 * 20.8)) as summary
                FROM employee e, salary s
                    WHERE e.id = s.employee_id;*/
            IsCalculated = true;
            throw new NotImplementedException();
        }
    }
}