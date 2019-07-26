using System;

namespace salary.dal
{
    public class EmployeePaymentKind
    {
        public static string cKind = "kind";
        public Guid EmployeeId { get; set; }
        public string PaymentKind { get; set; }
    }
}