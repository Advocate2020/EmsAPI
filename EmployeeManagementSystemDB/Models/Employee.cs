using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystemDB.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string EmpNo { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public required string IdNumber { get; set; }

        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public Employee()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}