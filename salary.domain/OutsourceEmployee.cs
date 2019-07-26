namespace salary.domain
{
    public class OutsourceEmployee : EmployeeBase
    {
        public override string Kind { get; protected set; } = EmployeeBase.KindHourly;

        public OutsourceEmployee()
        {
            
        }
        public override double CalculateSalary()
        {
            return Rate * 8 * 20.8;
        }
    }
}