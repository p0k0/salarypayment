using System;
using System.Text;
using MySql.Data.MySqlClient;
using salary.dal.repository.events;

namespace salary.dal.repository.commands
{
    public class GetManyEmployeeCommand : DbCommand
    {
        private readonly long _offset;
        private readonly long _limit;
        private readonly StringBuilder _stringBuilder;

        public event EventHandler<EmployeeEventArg> EmployeeRecieved;

        public GetManyEmployeeCommand(long offset, long limit, MySqlConnection connection) : base(connection)
        {
            _offset = offset;
            _limit = limit;
            _stringBuilder = new StringBuilder();
        }

        private string CreateQuery()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("select ");
            _stringBuilder.Append($"{Employee.cTableName}.{Employee.cName},");
            _stringBuilder.Append($"{Salary.cTableName}.{Salary.cRate},");
            _stringBuilder.Append($"{Salary.cTableName}.{Salary.cKind} ");
            
            _stringBuilder.Append($"from {Employee.cTableName},{Salary.cTableName} ");
            _stringBuilder.Append($"order by {Employee.cTableName}.{Employee.cName} ");
            _stringBuilder.Append($"limit {_limit} offset {_offset};");
            
            return _stringBuilder.ToString();
        }

        private void OnEmployeeRecieved(salary.dto.Employee dto)
        {
            if (EmployeeRecieved != null)
            {
                EmployeeRecieved(this, new EmployeeEventArg { Employee = dto });
            }
        }

        public override void Execute()
        {
            _connection.Open();
            var cmd = new MySqlCommand(CreateQuery(), _connection);
            var dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var result = new dto.Employee
                {
                    Name = dataReader[Employee.cName].ToString(),
                    Rate = Decimal.ToDouble(dataReader.GetDecimal(dataReader[Salary.cRate].ToString())),
                    Kind = dataReader[Salary.cKind].ToString()
                };
                OnEmployeeRecieved(result);
            }
            
            _connection.Close();
        }
    }
}