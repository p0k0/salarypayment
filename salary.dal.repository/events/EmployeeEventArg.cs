using System;

namespace salary.dal.repository.events
{
    public class EmployeeEventArg : EventArgs
    {
        public salary.dto.Employee Employee { get; set; }
    }
}