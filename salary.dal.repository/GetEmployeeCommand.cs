using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace salary.dal.repository
{
    public class GetEmployeeCommand : DbCommand
    {
        private readonly string _name;
        private static string _getEmployeeProcedureName = "get_employee";
        public salary.dto.Employee Result;
        
        public GetEmployeeCommand(string name, MySqlConnection connection) : base(connection)
        {
            _name = name;
        }

        public override void Execute()
        {
            _connection.Open();
            var cmd = new MySqlCommand(_getEmployeeProcedureName, _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", _name);
            
            var dataReader = cmd.ExecuteReader();
            dataReader.Read();

            var result = new dto.Employee
            {
                Name = dataReader[Employee.cName].ToString(),
                Rate = Decimal.ToDouble(dataReader.GetDecimal(dataReader[Salary.cRate].ToString())),
                Kind = dataReader[EmployeePaymentKind.cKind].ToString()
            };

            Result = result; 

            _connection.Close();
        }
    }
}