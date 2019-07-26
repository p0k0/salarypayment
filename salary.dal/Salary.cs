using System;

namespace salary.dal
{
    public class Salary
    {
        public static string cRate = "rate";
        public Guid EmployeeId { get; set; }
        public double Rate { get; set; }
    }
}