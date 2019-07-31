using System;
using System.Text;
using CommandLine;
using RestSharp;
using salary.cli.arguments;
using salary.dto;

namespace salary.cli
{
    internal sealed class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default
                .ParseArguments<SetHostAddress, GetEmployee, GetEmployees, GetTotalSalary, SetEmployee>(args)
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
            var client = new RestClient(opts.BaseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            
            
            var request = new RestRequest("api/v1/service/employees/{name}", Method.GET);
            request.AddUrlSegment("name", opts.CustomEmployeeName);

            var response = client.Execute<Employee>(request);
            var sb = new StringBuilder();
            
            
            sb.Append(nameof(response.Data.Id)).Append("|");
            sb.Append(nameof(response.Data.Name)).Append("|");
            sb.Append(nameof(response.Data.Kind)).Append("|");
            sb.Append(nameof(response.Data.Rate));
            sb.Append(Environment.NewLine);
            
            sb.Append(response.Data.Id).Append("|");
            sb.Append(response.Data.Name).Append("|");
            sb.Append(response.Data.Kind).Append("|");
            sb.Append(response.Data.Rate);
            sb.Append(Environment.NewLine);
            
            Console.WriteLine(sb.ToString());
            return 0;
        }
        
        private static int RunGetCommandAndReturnExitCode(GetEmployees opts)
        {
            
            return 0;
        }
    }
}
