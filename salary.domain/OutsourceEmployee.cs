namespace salary.domain
{
    public sealed class OutsourceEmployee : EmployeeBase
    {
        public override string Kind { get; protected set; } = EmployeeBase.KindHourly;

        public override double CalculateSalary()
        {
            return Rate * 8 * 20.8;
        }
    }
}