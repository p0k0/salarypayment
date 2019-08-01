using System;

namespace salary.dto
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public double Rate { get; set; }
        public override string ToString()
        {
            return ToString("|");
        }
        
        public string ToString(string separator)
        {
            return $"{Id}{separator}{Name}{separator}{Kind}{separator}{Rate}";
        }
        
        public string ToShortString(string separator)
        {
            return $"{Name}{separator}{Kind}{separator}{Rate}";
        }
    }
}
