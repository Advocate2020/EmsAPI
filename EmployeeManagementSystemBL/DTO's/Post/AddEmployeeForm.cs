using EmployeeManagementSystemDB.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemBL.DTO_s.Post
{
    public class AddEmployeeForm
    {
        [Required]
        [SwaggerSchema("The user's first name.")]
        public required string Name { get; set; }

        [Required]
        [SwaggerSchema("The user's last name.")]
        public required string Surname { get; set; }

        [Required]
        [Phone]
        [SwaggerSchema("The user's phone number.")]
        public required string PhoneNumber { get; set; }

        public User Map(string firebaseId, string userEmail)
        {
            return new User(firebaseId, userEmail)
            {
                FirebaseId = firebaseId,
                Employee = new()
                {
                    FirstName = Name,
                    LastName = Surname,
                    Phone = PhoneNumber,
                    Email = userEmail,
                }
            };
        }
    }
}