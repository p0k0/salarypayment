namespace salary.domain
{
    public class OfficeEmployee : EmployeeBase
    {
        public override double CalculateSalary()
        {
            return Rate;
        }
    }
}