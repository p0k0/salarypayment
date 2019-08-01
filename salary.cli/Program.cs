using System;
using System.Collections.Generic;
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
                .ParseArguments<SetHostAddress, GetEmployee, GetEmployeeWithMaxHourlyRate, GetEmployees, GetTotalSalary, SetEmployee>(args)
                .MapResult(
                    (SetEmployee opts) => RunSetCommandAndReturnExitCode(opts),
                    (GetEmployee opts) => GetEmployeeVerb(opts),
                    (GetEmployeeWithMaxHourlyRate opts) => GetEmployeeWithMaxHourlyRateVerb(opts),
                    (GetEmployees opts) => RunGetCommandAndReturnExitCode(opts),
                    (GetTotalSalary opts) => RunGetCommandAndReturnExitCode(opts),
                    errors => 1);
        }

        private static int GetEmployeeWithMaxHourlyRateVerb(GetEmployeeWithMaxHourlyRate opts)
        {
            var client = new RestClient(opts.BaseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            
            var request = new RestRequest("api/v1/service/rate/hourly/max", Method.GET);
            
            var response = client.Execute<Employee>(request);
            Console.Write(response.Data);
            return 0;
        }

        private static int RunSetCommandAndReturnExitCode(SetEmployee opts)
        {
            throw new NotImplementedException();
        }

        private static int RunGetCommandAndReturnExitCode(GetTotalSalary opts)
        {
            var client = new RestClient(opts.BaseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            
            var request = new RestRequest("api/v1/service/salary/sum", Method.GET);
            var response = client.Execute<double>(request);
            Console.Write(response.Data);
            return 0;
        }
        
        private static int GetEmployeeVerb(GetEmployee opts)
        {
            var client = new RestClient(opts.BaseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            
            var request = new RestRequest("api/v1/service/employees/{name}", Method.GET);
            request.AddUrlSegment("name", opts.CustomEmployeeName);
            var response = client.Execute<Employee>(request);
            Console.Write(response.Data);
            return 0;
        }
        
        private static int RunGetCommandAndReturnExitCode(GetEmployees opts)
        {
            var client = new RestClient(opts.BaseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            
            var request = new RestRequest("api/v1/service/employees", Method.GET);
            request.AddQueryParameter("limit", opts.Limit.ToString());
            request.AddQueryParameter("offset", opts.Offset.ToString());
            var response = client.Execute<List<Employee>>(request);
            response.Data.ForEach(Console.WriteLine);
            return 0;
        }
    }
}
