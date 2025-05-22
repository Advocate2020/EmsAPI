using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystemDB.Models
{
    [Index(nameof(FirebaseId), IsUnique = true)]
    public class User
    {
        public User(int id, string firebaseId, DateTime dateCreated, DateTime dateModified, int empID)
        {
            Id = id;
            FirebaseId = firebaseId;
            DateCreated = dateCreated;
            DateModified = dateModified;
            EmployeeId = empID;
        }

        /// <summary>
        /// Create account.
        /// </summary>
        public User(string firebaseId)
        {
            IsActive = true;
            FirebaseId = firebaseId;
            DateCreated = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        public string FirebaseId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}