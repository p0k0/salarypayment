using System;
using salary.dal;

namespace salary.service.command
{
    public class GetTotalSalarySumCommand : Command
    {
        public double Result
        {
            get
            {
                if (!IsCalculated)
                {
                    Execute();
                }

                return _salarySum;
            }
        }

        public bool IsCalculated { get; set; }

        public GetTotalSalarySumCommand(IRepository repository) : base(repository)
        {
        }
        

        public override void Execute()
        {
            base.Execute();
            _salarySum = _repository.GetMonthlyCost();
        }
         
        private double _salarySum;
    }
}