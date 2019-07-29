using System;

namespace salary.dal
{
    public class Employee
    {
        public static string cTableName = "employee";
        public static string cName = "name";
        public static string cId = "id";
        
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
}