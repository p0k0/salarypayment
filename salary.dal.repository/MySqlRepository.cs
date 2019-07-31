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
        private MySqlConnection _connection;


        public MySqlRepository(string connectionString)
        {
            Contract.Assume(!string.IsNullOrEmpty(connectionString));
            _connectionString = connectionString;
        }
        
        public dto.Employee Get(string name)
        {
            using (_connection = new MySqlConnection(_connectionString))
            {
                _connection.Open();
                var cmd = new GetEmployeeCommand(name, _connection);
                cmd.Execute();
                _connection.Close();
                return cmd.Result;
            }
        }

        public bool Save(dto.Employee employee)
        {
            using (_connection = new MySqlConnection(_connectionString))
            {
                _connection.Open();
                var cmd = new SaveEmployeeCommand(employee, _connection);
                cmd.Execute();
                _connection.Close();
                return cmd.IsSuccess;
            }
        }

        public IEnumerable<dto.Employee> GetMany(long limit, long offset)
        {
            using (_connection = new MySqlConnection(_connectionString))
            {
                _connection.Open();
                var cmd = new GetManyEmployeeCommand( limit, offset, _connection);
                cmd.Execute();
                _connection.Close();
                return cmd.Employees;
            }
        }

        public dto.Employee GetEmployeeWithMaxHourlyRate()
        {
            using (_connection = new MySqlConnection(_connectionString))
            {
                _connection.Open();
                var cmd = new GetEmployeeWithMaxHourlyRateCommand(_connection);
                cmd.Execute();
                _connection.Close();
                return cmd.Result;
            }
        }

        public double GetMonthlyCost()
        {
            using (_connection = new MySqlConnection(_connectionString))
            {
                _connection.Open();
                var cmd = new GetTotalSalarySumCommand(_connection);
                cmd.Execute();
                _connection.Close();
                return cmd.Result;
            }
        }
    }
}