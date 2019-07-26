using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using MySql.Data.MySqlClient;
using salary.dal.repository.command;

namespace salary.dal.repository
{
    public class MySQLRepository : IRepository
    {
        private readonly string _connectionString;
        private MySqlConnection _connection;


        public MySQLRepository(string connectionString)
        {
            Contract.Assume(!string.IsNullOrEmpty(connectionString));
            _connectionString = connectionString;
        }

        public MySqlConnection Connection => _connection ?? (_connection = new MySqlConnection(_connectionString));

        public dto.Employee Get(string name)
        {
            var cmd = new GetEmployeeCommand(name, _connection);
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return cmd.Result;
        }

        public bool Save(dto.Employee employee)
        {
            var cmd = new SaveEmployeeCommand(employee, _connection);
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return cmd.IsSuccess;
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