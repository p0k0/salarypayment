using System;
using System.Text;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetTotalSalarySumCommand : DbCommand
    {
        private double _salarySum;
        private StringBuilder _stringBuilder;
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
            _stringBuilder = new StringBuilder();
        }
        
        private string CreateQuery()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append($"select sum(if (s.{Salary.cKind} like 'monthly', s.{Salary.cRate}, s.{Salary.cRate} * 8.0 * 20.8)) as summary ");
            _stringBuilder.Append($"from {Employee.cTableName} e,{Salary.cTableName} s ");
            _stringBuilder.Append($"where e.{Employee.cId}=s.{Salary.cEmployeeId};");
            return _stringBuilder.ToString();
        }

        public override void Execute()
        {
            /*
             SELECT sum(if (s.kind like 'monthly', s.rate, s.rate * 8.0 * 20.8)) as summary
             FROM employee e, salary s
             WHERE e.id = s.employee_id;
             */
            _connection.Open();
            var cmd = new MySqlCommand(CreateQuery(), _connection);
            _salarySum = (double) cmd.ExecuteScalar();
            IsCalculated = true;
            _connection.Close();
        }
    }
}