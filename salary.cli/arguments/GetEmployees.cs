using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace salary.cli.arguments
{
    [Verb("get-employees", HelpText = "Get employees info")]
    public class GetEmployees: SetHostAddress
    {
        [Option('o', "offset", Required = false, Default = 0,HelpText = "Result offset.")]
        public long Offset { get; set; }

        [Option('l', "limit", Required = false, Default = 5,HelpText = "Result limit.")]
        public long Limit { get; set; }
        
        [Usage(ApplicationAlias = "salary.cli")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Get 10 employees offset 40 employee. Employee ordered by salary payment desc and employee name asc",
                    new GetEmployees
                    {
                        Limit = 10,
                        Offset = 40,
                        BaseUrl = "https://localhost:5001/"
                    });
            }
        }
    }
}