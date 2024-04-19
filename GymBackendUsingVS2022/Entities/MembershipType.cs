using GymBackendUsingVS2022.Models;

namespace GymBackendUsingVS2022.Entities
{
    public class MembershipType
    {
        public int MembershipTypeId { get; set; }
        public required string MembershipName { get; set;}
        public string? MembershipDescription { get; set;}

        public int Duration { get; set; }
        public decimal Price { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<ProgramList>? ProgramLists { get; set; }
        public List<Membership>? Memberships { get; set; }
    }
}
