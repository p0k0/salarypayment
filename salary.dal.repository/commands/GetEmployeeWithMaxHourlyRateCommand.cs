using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetEmployeeWithMaxHourlyRateCommand : DbCommand
    {
        private dto.Employee _employee;
        private StringBuilder _stringBuilder;

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
        public GetEmployeeWithMaxHourlyRateCommand(MySqlConnection connection) : base(connection)
        {
            _stringBuilder = new StringBuilder();
        }
        
        private string CreateQuery()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("select ");
            _stringBuilder.Append($"{Employee.cTableName}.{Employee.cId},");
            _stringBuilder.Append($"{Employee.cTableName}.{Employee.cName},");
            _stringBuilder.Append($"{Salary.cTableName}.{Salary.cKind},");
            _stringBuilder.Append($"max({Salary.cTableName}.{Salary.cRate}) as {Salary.cRate} ");
            
            _stringBuilder.Append($"from {Employee.cTableName},{Salary.cTableName} ");
            _stringBuilder.Append($"where {Employee.cTableName}.{Employee.cId}={Salary.cTableName}.{Salary.cEmployeeId} ");
            _stringBuilder.Append($"and {Salary.cTableName}.{Salary.cKind} = '{Salary.KindHourly}';");
            
            return _stringBuilder.ToString();
        }
        
        

        private void Initialize(MySqlCommand cmd)
        {
            cmd.CommandText = CreateQuery();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new MySqlParameter($"@{Employee.cId}", MySqlDbType.VarBinary, 16));
            cmd.Parameters.Add(new MySqlParameter($"@{Employee.cName}", MySqlDbType.VarChar, 20));
            cmd.Parameters.Add(new MySqlParameter($"@{Salary.cRate}", MySqlDbType.Decimal));
            cmd.Parameters.Add(new MySqlParameter($"@{Salary.cKind}", MySqlDbType.VarChar, 10));

            cmd.Parameters[$"@{Employee.cId}"].Direction = ParameterDirection.Output;
            cmd.Parameters[$"@{Employee.cName}"].Direction = ParameterDirection.Output;
            cmd.Parameters[$"@{Salary.cRate}"].Direction = ParameterDirection.Output;
            cmd.Parameters[$"@{Salary.cKind}"].Direction = ParameterDirection.Output;
        }

        public override void Execute()
        {
            /*
             SELECT e.id, e.name, s.kind, max(s.rate)
             FROM salary s, employee e
             WHERE e.id = s.employee_id and s.kind = 'hourly';
             */
            base.Execute();
            using (var cmd = _connection.CreateCommand())
            {
                Initialize(cmd);
                using (var dataReader = cmd.ExecuteReader())
                {
                    dataReader.Read();
                    _employee = dataReader.ReadEmployee();
                }
            }
        }
    }
}