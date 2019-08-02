using System;
using System.Collections.Generic;
using CommandLine;
using RestSharp;
using salary.cli.arguments;
using salary.dto;

namespace salary.cli
{
    internal sealed class Program
    {
        private const string _path = "api/v1/service/";
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
            
            var request = new RestRequest(_path + "rate/hourly/max", Method.GET);
            
            var response = client.Execute<Employee>(request);
            Console.Write(response.Data);
            return 0;
        }

        private static int RunSetCommandAndReturnExitCode(SetEmployee opts)
        {
            var client = new RestClient(opts.BaseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            
            var request = new RestRequest(_path + "employees", Method.POST);
            request.AddJsonBody(new salary.host.webapi.dto.Employee()
            {
                Name = opts.CustomEmployeeName, 
                Kind = opts.PaymentKind,
                Rate = opts.Rate.ToString()
            });
            var response = client.Execute<bool>(request);
            Console.Write($"isSuccess:{response.Data}");
            return 0;
        }

        private static int RunGetCommandAndReturnExitCode(GetTotalSalary opts)
        {
            var client = new RestClient(opts.BaseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            };
            
            var request = new RestRequest(_path + "salary/sum", Method.GET);
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
            
            var request = new RestRequest(_path + "employees/{name}", Method.GET);
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
            
            var request = new RestRequest(_path + "employees", Method.GET);
            request.AddQueryParameter("limit", opts.Limit.ToString());
            request.AddQueryParameter("offset", opts.Offset.ToString());
            var response = client.Execute<List<Employee>>(request);
            response.Data.ForEach(Console.WriteLine);
            return 0;
        }
    }
}
