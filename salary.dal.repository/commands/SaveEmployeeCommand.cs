using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class SaveEmployeeCommand : DbCommand
    {
        private readonly dto.Employee _employee;

        public bool IsSuccess;

        public SaveEmployeeCommand(salary.dto.Employee employee, MySqlConnection connection) : base(connection)
        {
            _employee = employee;
        }

        private void Initialize(MySqlCommand cmd)
        {
            cmd.CommandText = $"call save_employee(@{Employee.cName}, @{Salary.cRate}, @{Salary.cKind});";
            cmd.CommandType = CommandType.Text;
            
            cmd.Parameters.Add(new MySqlParameter($"@{Employee.cName}", MySqlDbType.VarChar, 20));
            cmd.Parameters.Add(new MySqlParameter($"@{Salary.cRate}", MySqlDbType.Decimal));
            cmd.Parameters.Add(new MySqlParameter($"@{Salary.cKind}", MySqlDbType.VarChar, 10));
            
            cmd.Parameters[$"@{Employee.cName}"].Direction = ParameterDirection.Input;
            cmd.Parameters[$"@{Salary.cRate}"].Direction = ParameterDirection.Input;
            cmd.Parameters[$"@{Salary.cKind}"].Direction = ParameterDirection.Input;

            cmd.Parameters[$"@{Employee.cName}"].Value = _employee.Name;
            cmd.Parameters[$"@{Salary.cRate}"].Value = _employee.Rate;
            cmd.Parameters[$"@{Salary.cKind}"].Value = _employee.Kind;
        }
        
        public override void Execute()
        {
            base.Execute();

            using (var transaction = _connection.BeginTransaction(IsolationLevel.Serializable))
            {
                using (var cmd = new MySqlCommand(string.Empty, _connection, transaction))
                {
                    Initialize(cmd);
                    try
                    {
                        if (cmd.ExecuteNonQuery() >= 1)
                        {
                            IsSuccess = true;
                            transaction.Commit();
                            return;
                        }
                        transaction.Rollback();
                        
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}