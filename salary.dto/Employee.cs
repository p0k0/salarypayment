using System;

namespace salary.dto
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public string Kind { get; set; }
    }
}
