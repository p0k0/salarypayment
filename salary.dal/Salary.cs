using System;

namespace salary.dal
{
    public class Salary
    {
        public static string cTableName = "salary";
        public static string cRate = "rate";
        public static string cKind = "kind";
        
        public Guid EmployeeId { get; set; }
        public double Rate { get; set; }
        public string PaymentKind { get; set; }
    }
}