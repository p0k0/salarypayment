namespace salary.domain
{
    public class OutsourceEmployee : EmployeeBase
    {
        public override double CalculateSalary()
        {
            return Rate * 8 * 20.8;
        }
    }
}