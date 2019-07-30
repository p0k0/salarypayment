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
                var cmd = new GetEmployeeCommand(name, _connection);
                cmd.Execute();
                return cmd.Result;
            }
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

        public IEnumerable<dto.Employee> GetMany(long limit, long offset)
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

        public dto.Employee GetEmployeeWithMaxHourlySalary()
        {
            var cmd = new GetEmployeeWithMaxHourlySalaryCommand(_connection);
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

        public double GetMonthlyCost()
        {
            var cmd = new GetTotalSalarySumCommand(_connection);
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
    }
}