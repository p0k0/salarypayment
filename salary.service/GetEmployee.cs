using salary.dal;

namespace salary.service
{
    public sealed class GetEmployee : Command
    {
        public GetEmployee(IRepository repository) : base(repository)
        {
        }
        
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}