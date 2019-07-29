using System;
using System.Text;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetEmployeeWithMaxHourlySalaryCommand : DbCommand
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
        public GetEmployeeWithMaxHourlySalaryCommand(MySqlConnection connection) : base(connection)
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
            _stringBuilder.Append($"max({Salary.cTableName}.{Salary.cRate}) ");
            
            _stringBuilder.Append($"from {Employee.cTableName},{Salary.cTableName} ");
            _stringBuilder.Append($"where {Employee.cTableName}.{Employee.cId}={Salary.cTableName}.{Salary.cEmployeeId} ");
            _stringBuilder.Append($"and {Salary.cTableName}.{Salary.cKind} = {Salary.KindHourly};");
            
            return _stringBuilder.ToString();
        }

        public override void Execute()
        {
            /*
             SELECT e.id, e.name, s.kind, max(s.rate)
             FROM salary s, employee e
             WHERE e.id = s.employee_id and s.kind = 'hourly';
             */
            _connection.Open();
            var cmd = new MySqlCommand(CreateQuery(), _connection);
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                _employee = new dto.Employee
                {
                    Id = Guid.Parse(dataReader[Employee.cId].ToString()),
                    Name = dataReader[Employee.cName].ToString(),
                    Kind = dataReader[Salary.cKind].ToString(),
                    Rate = Decimal.ToDouble(dataReader.GetDecimal(dataReader[Salary.cRate].ToString()))
                };
            }
            
            _connection.Close();
        }
    }
}