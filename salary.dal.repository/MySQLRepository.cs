using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using MySql.Data.MySqlClient;

namespace salary.dal.repository
{
    public class MySQLRepository : IRepository
    {
        private readonly string _connectionString;
        private MySqlConnection _connection;

        private static string _saveEmployeeProcedureName = "save_employee";
        private static string _getEmployeeProcedureName = "get_employee";
        
        public MySQLRepository(string connectionString)
        {
            Contract.Assume(!string.IsNullOrEmpty(connectionString));
            _connectionString = connectionString;
        }

        public MySqlConnection Connection => _connection ?? (_connection = new MySqlConnection(_connectionString));

        public dto.Employee Get(string name)
        {
            Connection.Open();
            var cmd = new MySqlCommand(_getEmployeeProcedureName, _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);

            var result = new dto.Employee();
            var dataReader = cmd.ExecuteReader();
            dataReader.Read();


            result.Name = dataReader[Employee.cName].ToString();
            result.Rate = Decimal.ToDouble(dataReader.GetDecimal(dataReader[Salary.cRate].ToString()));
            result.Kind = dataReader[EmployeePaymentKind.cKind].ToString();

            return result;
        }

        public void Save(dto.Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<dto.Employee> GetMany(int skip, int limit)
        {
            throw new System.NotImplementedException();
        }

        public dto.Employee GetMostExpensiveEmployee(short kind)
        {
            throw new System.NotImplementedException();
        }

        public double GetMonthlyCost()
        {
            throw new System.NotImplementedException();
        }
    }
}