using System;
using System.Data;
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

        private void Initialize(MySqlCommand cmd)
        {
            cmd.CommandText = CreateQuery();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new MySqlParameter("@summary", MySqlDbType.Decimal));
            cmd.Parameters["@summary"].Direction = ParameterDirection.Output;
        }

        public override void Execute()
        {
            /*
             SELECT sum(if (s.kind like 'monthly', s.rate, s.rate * 8.0 * 20.8)) as summary
             FROM employee e, salary s
             WHERE e.id = s.employee_id;
             */
            base.Execute();

            using (var cmd = _connection.CreateCommand())
            {
                Initialize(cmd);
                _salarySum = Decimal.ToDouble((decimal)cmd.ExecuteScalar());
                IsCalculated = true;
            }
        }
    }
}