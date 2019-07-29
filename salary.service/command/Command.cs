using System;
using salary.dal;

namespace salary.service.command
{
    public class Command
    {
        protected readonly IRepository _repository;

        public Command(IRepository repository)
        {
            _repository = repository;
        }

        public virtual void Execute()
        {
            
        }
    }
}