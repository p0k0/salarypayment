using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetManyEmployeeCommand : DbCommand
    {
        private readonly long _offset;
        private readonly long _limit;
        private readonly StringBuilder _stringBuilder;
        private LinkedList<dto.Employee> _employees;

        public LinkedList<dto.Employee> Employees => _employees;
        
        public GetManyEmployeeCommand(long limit, long offset, MySqlConnection connection) : base(connection)
        {
            _offset = offset;
            _limit = limit;
            _stringBuilder = new StringBuilder();
        }

        private string CreateQuery()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("select ");
            _stringBuilder.Append($"{Employee.cTableName}.{Employee.cId},");
            _stringBuilder.Append($"{Employee.cTableName}.{Employee.cName},");
            _stringBuilder.Append($"{Salary.cTableName}.{Salary.cRate},");
            _stringBuilder.Append($"{Salary.cTableName}.{Salary.cKind},");
            
            _stringBuilder.Append($"if ({Salary.cTableName}.{Salary.cKind} like '{Salary.KindMonthly}',{Salary.cTableName}.{Salary.cRate},");
            _stringBuilder.Append($"{Salary.cTableName}.{Salary.cRate} * 8.0 * 20.8) as p ");
            
            _stringBuilder.Append($"from {Employee.cTableName},{Salary.cTableName} ");
            _stringBuilder.Append($"where {Employee.cTableName}.{Employee.cId}={Salary.cTableName}.{Salary.cEmployeeId} ");
            
            _stringBuilder.Append($"order by p desc, {Employee.cTableName}.{Employee.cName} ");
            _stringBuilder.Append($"limit {_limit} offset {_offset};");
            
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
            base.Execute();
            using (var cmd = _connection.CreateCommand())
            {
                Initialize(cmd);
                using (var dataReader = cmd.ExecuteReader())
                {
                    _employees = new LinkedList<dto.Employee>();
                    while (dataReader.Read())
                    {
                        _employees.AddLast(dataReader.ReadEmployee());
                    }
                }
            }
            
        }
    }
}