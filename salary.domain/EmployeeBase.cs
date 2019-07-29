using System;

namespace salary.domain
{
    public abstract class EmployeeBase
    {
        public static string KindMonthly = "monthly";
        public static string KindHourly = "hourly";
        
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public double Rate { get; set; }
        
        public abstract string Kind { get; protected set; }
        public abstract double CalculateSalary();
    }
}