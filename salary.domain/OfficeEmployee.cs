namespace salary.domain
{
    public class OfficeEmployee : EmployeeBase
    {
        
        public override string Kind { get; protected set; } = EmployeeBase.KindMonthly;

        public OfficeEmployee()
        {
            
        }
        
        public override double CalculateSalary()
        {
            return Rate;
        }
    }
}