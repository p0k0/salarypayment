using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace salary.cli.arguments
{
    [Verb("set-employee", HelpText = "Set new employee with salary")]
    public class SetEmployee : SetHostAddress
    {
        [Option('n', "name", Required = true, HelpText = "Employee name.")]
        public string CustomEmployeeName { get; set; }

        [Option('k', "kind", Required = true, HelpText = "Salary payment kind.")]
        public string PaymentKind { get; set; }
        
        [Option('r', "rate", Required = true, HelpText = "Salary payment rate.")]
        public double Rate { get; set; }
        
        [Usage(ApplicationAlias = "salary.cli")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Set new employee with hourly payment kind",
                    new SetEmployee
                    {
                        CustomEmployeeName = "Sam",
                        PaymentKind = "hourly",
                        Rate = 1250
                    });
                yield return new Example("Set new employee with monthly payment kind",
                    new SetEmployee
                    {
                        CustomEmployeeName = "Will",
                        PaymentKind = "monthly",
                        Rate = 125000
                    });
            }
        }
    }
}