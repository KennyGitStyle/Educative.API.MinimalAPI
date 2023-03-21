using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educative.Core.Entity
{
    public class Address
    {
        public string AddressID { get; set; } = string.Empty!;
        [Display(Name = "Address Line 1")]
        public  string Addr1 { get; set; } = string.Empty!;
        [Display(Name = "Address Line 2")]
        public string Add2 { get; set; } = string.Empty!;
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty!;
        [Display(Name = "County")]
        public string County { get; set; } = string.Empty!;
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; } = string.Empty!;
        [ForeignKey("Student")]
        public string StudentAddressID { get; set; } = string.Empty!;
        public virtual Student? Student { get; set; }
    }
}