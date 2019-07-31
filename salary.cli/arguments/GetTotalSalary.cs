using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace salary.cli.arguments
{
    [Verb("get-salary", HelpText = "Get salary info")]
    public class GetTotalSalary: SetHostAddress
    {
        [Option('t', "totalmonthpayment", Required = true, HelpText = "Total month salary payment.")]
        public bool TotalMonthPayment { get; set; }
        
        
        [Usage(ApplicationAlias = "salary.cli")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Get total month salary payment",
                    new GetTotalSalary
                    {
                        TotalMonthPayment = true
                    });
            }
        }
    }
}