using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using MySql.Data.MySqlClient;
using salary.dal.repository.commands;

namespace salary.dal.repository
{
    public sealed class MySqlRepository : IRepository
    {
        private readonly string _connectionString;

        public MySqlRepository(string connectionString)
        {
            Contract.Assume(!string.IsNullOrEmpty(connectionString));
            _connectionString = connectionString;
        }
        
        public dto.Employee Get(string name)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new GetEmployeeCommand(name, connection);
                cmd.Execute();
                connection.Close();
                return cmd.Result;
            }
        }

        public bool Save(dto.Employee employee)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SaveEmployeeCommand(employee, connection);
                cmd.Execute();
                connection.Close();
                return cmd.IsSuccess;
            }
        }

        public IEnumerable<dto.Employee> GetMany(long limit, long offset)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new GetManyEmployeeCommand( limit, offset, connection);
                cmd.Execute();
                connection.Close();
                return cmd.Employees;
            }
        }

        public dto.Employee GetEmployeeWithMaxHourlyRate()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new GetEmployeeWithMaxHourlyRateCommand(connection);
                cmd.Execute();
                connection.Close();
                return cmd.Result;
            }
        }

        public double GetMonthlyCost()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new GetTotalSalarySumCommand(connection);
                cmd.Execute();
                connection.Close();
                return cmd.Result;
            }
        }
    }
}