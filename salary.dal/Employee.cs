using System;

namespace salary.dal
{
    public class Employee
    {
        public const string cTableName = "employee";
        public const string cName = "name";
        public const string cId = "id";
        
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
}