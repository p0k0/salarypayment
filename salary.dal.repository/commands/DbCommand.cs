using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class DbCommand
    {
        private double _delay = 250;
        private short _maxRetry = 5;
        private short _retry = 0;
        protected readonly MySqlConnection _connection;
        
        public DbCommand(MySqlConnection connection)
        {
            _connection = connection;
        }
        
        public virtual void Execute()
        {
        }
    }
}