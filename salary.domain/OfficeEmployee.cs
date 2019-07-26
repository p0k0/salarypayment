namespace salary.domain
{
    public sealed class OfficeEmployee : EmployeeBase
    {
        public override string Kind { get; protected set; } = EmployeeBase.KindMonthly;

        public override double CalculateSalary()
        {
            return Rate;
        }
    }
}