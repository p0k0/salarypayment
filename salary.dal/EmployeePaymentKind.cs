using System;

namespace salary.dal
{
    public class EmployeePaymentKind
    {
        public Guid EmployeeId { get; set; }
        public short PaymentKind { get; set; }
    }
}