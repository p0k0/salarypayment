using System;

namespace salary.dal
{
    public class Salary
    {
        public const string KindHourly = "hourly";
        public const string KindMonthly = "monthly";
        
        public const string cTableName = "salary";
        public const string cRate = "rate";
        public const string cEmployeeId = "employee_id";
        public const string cKind = "kind";
        
        public Guid EmployeeId { get; set; }
        public double Rate { get; set; }
        public string PaymentKind { get; set; }
    }
}