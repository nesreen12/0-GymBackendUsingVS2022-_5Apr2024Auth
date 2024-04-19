using GymBackendUsingVS2022.Entities;
using System.ComponentModel.DataAnnotations;

namespace GymBackendUsingVS2022.Models
{
    public class ProgramList
    {
        [Key]
        public int ProgramListId { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }

        

        public List<WorkoutPlan>? WorkoutPlans { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<MembershipType>? MembershipTypes { get; set; }
    }
}
