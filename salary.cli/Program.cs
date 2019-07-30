using System;
using CommandLine;

namespace salary.cli
{
    internal sealed class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default
                .ParseArguments<GetEmployee, GetEmployees, GetTotalSalary, SetEmployee>(args)
                .MapResult(
                    (SetEmployee opts) => RunSetCommandAndReturnExitCode(opts),
                    (GetEmployee opts) => RunGetCommandAndReturnExitCode(opts),
                    (GetEmployees opts) => RunGetCommandAndReturnExitCode(opts),
                    (GetTotalSalary opts) => RunGetCommandAndReturnExitCode(opts),
                    errors => 1);
        }

        private static int RunSetCommandAndReturnExitCode(SetEmployee opts)
        {
            throw new NotImplementedException();
        }

        private static int RunGetCommandAndReturnExitCode(GetTotalSalary opts)
        {
            return 0;
        }
        
        private static int RunGetCommandAndReturnExitCode(GetEmployee opts)
        {
            Console.WriteLine($"{opts.CustomEmployeeName}");
            return 0;
        }
        
        private static int RunGetCommandAndReturnExitCode(GetEmployees opts)
        {
            
            return 0;
        }
    }
}
