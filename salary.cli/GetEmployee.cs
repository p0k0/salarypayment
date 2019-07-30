using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace salary.cli
{
    [Verb("get-employee", HelpText = "Get employee by name, by max hourly rate")]
    public class GetEmployee
    {
        [Option('n', "name", Required = false, HelpText = "Employee name.")]
        public string CustomEmployeeName { get; set; }
        
        [Option('m', "max", Required = true, HelpText = "Max hourly salary payment.")]
        public bool MaxHourlyEmployee { get; set; }

        
        [Usage(ApplicationAlias = "salary.cli")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Get employee with name 'Sam'",
                    new GetEmployee
                    {
                        CustomEmployeeName = "sam"
                    });
                yield return new Example("Get employee with maximum hourly rate",
                    new GetEmployee
                    {
                        MaxHourlyEmployee = true
                    });
            }
        }
    }
}