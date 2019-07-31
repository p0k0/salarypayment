using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetEmployeeCommand : DbCommand
    {
        private readonly string _name;
        public salary.dto.Employee Result;
        
        public GetEmployeeCommand(string name, MySqlConnection connection) : base(connection)
        {
            _name = name;
        }

        public override void Execute()
        {
            base.Execute();
            using (var cmd = _connection.CreateCommand())
            {
                Initialize(cmd);
                using (var dataReader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    dataReader.Read();
                    Result = dataReader.ReadEmployee();
                }
            }
        }

        private void Initialize(MySqlCommand cmd)
        {
            cmd.CommandText = $"call get_employee('{_name}');";
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
    }
}