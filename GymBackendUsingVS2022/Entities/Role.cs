using Microsoft.AspNetCore.Identity;

namespace GymBackendUsingVS2022.Entities
{
    public class Role : IdentityRole
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set;}

        public  List<User>? Users { get; set; }
        


    }
}
