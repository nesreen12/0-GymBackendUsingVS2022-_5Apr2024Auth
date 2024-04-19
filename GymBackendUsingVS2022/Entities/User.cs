using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymBackendUsingVS2022.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        //public required string UserName { get; set; }
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; } 
        public required string Password { get; set; }
        [EmailAddress] 
        public required string EmailAddress { get; set; }

        
        // Foreign Key
        public required string RoleId { get; set; }

        // Navigation Property
        public  Role? Role { get; set; }
        public Member? Member { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }






    }
}
