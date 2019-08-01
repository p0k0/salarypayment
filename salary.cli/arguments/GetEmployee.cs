using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace salary.cli.arguments
{
    [Verb("get-employee", HelpText = "Get employee by name")]
    public class GetEmployee: SetHostAddress
    {
        [Option('n', "name", Required = false, HelpText = "Employee name.")]
        public string CustomEmployeeName { get; set; }

        
        [Usage(ApplicationAlias = "salary.cli")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Get employee with name 'Sam'",
                    new GetEmployee
                    {
                        CustomEmployeeName = "sam",
                        BaseUrl = "https://localhost:5001/"
                    });
            }
        }
    }
}