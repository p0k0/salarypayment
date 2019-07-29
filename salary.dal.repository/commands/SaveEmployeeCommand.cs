using System.Data;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class SaveEmployeeCommand : DbCommand
    {
        private static string _saveEmployeeProcedureName = "save_employee";
        
        private readonly dto.Employee _employee;

        public bool IsSuccess;
        
        public SaveEmployeeCommand(salary.dto.Employee employee, MySqlConnection connection) : base(connection)
        {
            _employee = employee;
        }

        public override void Execute()
        {
            base.Execute();
            _connection.Open();
            var cmd = new MySqlCommand(_saveEmployeeProcedureName, _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@name", _employee.Name);
            cmd.Parameters.AddWithValue("@rate", _employee.Rate);
            cmd.Parameters.AddWithValue("@kind", _employee.Kind);
            
            IsSuccess = cmd.ExecuteNonQuery() > 1;
            
            _connection.Close();
        }
    }
}