using System;
using salary.dal;

namespace salary.service
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
            throw new NotImplementedException();
        }
    }
}