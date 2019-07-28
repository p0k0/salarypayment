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

        public IEnumerable<dto.Employee> GetMany(int offset, int limit)
        {
            var cmd = new GetManyEmployeeCommand(offset, limit, _connection);
            
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return cmd.Employees;
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