using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace salary.cli.arguments
{
    [Verb("get-employee-max-hourly-rate", HelpText = "Get employee by max hourly rate")]
    public class GetEmployeeWithMaxHourlyRate: SetHostAddress
    {
        [Option('m', "max", Required = false, HelpText = "Max hourly salary payment.")]
        public bool MaxHourlyEmployee { get; set; }

        
        [Usage(ApplicationAlias = "salary.cli")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Get employee with maximum hourly rate",
                    new GetEmployeeWithMaxHourlyRate
                    {
                        MaxHourlyEmployee = true,
                        BaseUrl = "https://localhost:5001/"
                    });
            }
        }
    }
}