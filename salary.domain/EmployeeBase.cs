namespace salary.domain
{
    public abstract class EmployeeBase
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public abstract double CalculateSalary();
    }
}