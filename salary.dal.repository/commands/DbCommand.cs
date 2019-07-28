using System;
using MySql.Data.MySqlClient;

namespace salary.dal.repository.commands
{
    public class DbCommand
    {
        protected readonly MySqlConnection _connection;
        
        public DbCommand(MySqlConnection connection)
        {
            _connection = connection;
        }
        
        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}