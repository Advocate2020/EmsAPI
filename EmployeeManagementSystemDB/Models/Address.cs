using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemDB.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public required string StreetAddress { get; set; }

        public required string Suburb { get; set; }

        public required string City { get; set; }

        public required string Province { get; set; }

        public required string PostalCode { get; set; }
    }
}