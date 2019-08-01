using System.ComponentModel.DataAnnotations;

namespace salary.host.webapi.dto
{
    public class Employee
    {
        public string Id { get; set; }
        
        //[MaxLength(20, ErrorMessage = "Max length is 20 symbols")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$", ErrorMessage = "Maximum 20-character are allowed.")]
        public string Name { get; set; }
        
        [RegularExpression(@"^[0-9]{1,20}$", ErrorMessage = "Maximum 20-digit number allowed.")]
        public string Rate { get; set; }
        
        [RegularExpression(@"^(monthly|hourly){1}$", ErrorMessage = "Only 'monthly' or 'hourly' are allowed.")]
        public string Kind { get; set; }
    }
}
