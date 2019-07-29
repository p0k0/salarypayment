using System;
using System.Collections.Generic;
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
            
            _stringBuilder.Append($"if ({Salary.cTableName}.{Salary.cKind} like '{Salary.Monthly}',{Salary.cTableName}.{Salary.cRate},");
            _stringBuilder.Append($"{Salary.cTableName}.{Salary.cRate} * 8.0 * 20.8) as p ");
            
            _stringBuilder.Append($"from {Employee.cTableName},{Salary.cTableName} ");
            _stringBuilder.Append($"where {Employee.cTableName},{Employee.cId}={Salary.cTableName}.{Salary.cEmployeeId} ");
            
            _stringBuilder.Append($"order by p desc, {Employee.cTableName}.{Employee.cName} ");
            _stringBuilder.Append($"limit {_limit} offset {_offset};");
            
            return _stringBuilder.ToString();
        }

        public override void Execute()
        {
            _connection.Open();
            var cmd = new MySqlCommand(CreateQuery(), _connection);
            var dataReader = cmd.ExecuteReader();
            _employees = new LinkedList<dto.Employee>();

            while (dataReader.Read())
            {
                var result = new dto.Employee
                {
                    Id = Guid.Parse(dataReader[Employee.cId].ToString()),
                    Name = dataReader[Employee.cName].ToString(),
                    Rate = Decimal.ToDouble(dataReader.GetDecimal(dataReader[Salary.cRate].ToString())),
                    Kind = dataReader[Salary.cKind].ToString()
                };
                _employees.Append(result);
            }
            
            _connection.Close();
        }
    }
}