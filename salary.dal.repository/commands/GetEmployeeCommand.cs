using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class GetEmployeeCommand : DbCommand
    {
        private readonly string _name;
        private static string _getEmployeeProcedureName = "call get_employee(`@inname`);";
        public salary.dto.Employee Result;
        
        public GetEmployeeCommand(string name, MySqlConnection connection) : base(connection)
        {
            _name = name;
        }

        public override void Execute()
        {
            //base.Execute();
            
            _connection.Open();
            
            var cmd = _connection.CreateCommand();
            cmd.CommandText = $"call get_employee('{_name}');";
            cmd.CommandType = CommandType.Text;
            
            cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarBinary, 16));
            cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar, 20));
            cmd.Parameters.Add(new MySqlParameter("@rate", MySqlDbType.Decimal));
            cmd.Parameters.Add(new MySqlParameter("@kind", MySqlDbType.VarChar, 10));
            
            cmd.Parameters["@id"].Direction = ParameterDirection.Output;
            cmd.Parameters["@name"].Direction = ParameterDirection.Output;
            cmd.Parameters["@rate"].Direction = ParameterDirection.Output;
            cmd.Parameters["@kind"].Direction = ParameterDirection.Output;
            
            MySqlDataReader dataReader;
            try
            {
                dataReader = cmd.ExecuteReader(CommandBehavior.SingleRow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
                
            dataReader.Read();

            var result = new dto.Employee();
            result.Id = new Guid(dataReader[Employee.cId] as byte[]);
            result.Name = dataReader[Employee.cName].ToString();
            result.Rate = Decimal.ToDouble(Decimal.Parse(dataReader[Salary.cRate].ToString()));
            result.Kind = dataReader[Salary.cKind].ToString();
            
            
            dataReader.Close();

            Result = result; 

            _connection.Close();
        }
    }
}