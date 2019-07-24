using salary.dal;

namespace salary.service
{
    public sealed class SaveEmployee : Command
    {
        public SaveEmployee(IRepository repository) : base(repository)
        {
        }
        
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}