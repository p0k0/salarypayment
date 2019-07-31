using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace salary.cli.arguments
{
    public class SetHostAddress
    {
        [Option('u', "url", Required = false, Default = "http://localhost:5000/",HelpText = "Web API baseUrl address.")]
        public string BaseUrl { get; set; }
    }
}