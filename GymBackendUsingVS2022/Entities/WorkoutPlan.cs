using GymBackendUsingVS2022.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymBackendUsingVS2022.Entities
{
    public enum Duration
    { 
       twoWeeks,
       threeWeeks,
       fourWeeks
    }
    public class WorkoutPlan
    {
        [Key]
        public int WorkoutPlanId { get; set; }
        public int ProgramListId { get; set; }
        public int InstructorId { get; set; }

        public required string  WorkoutPlanName { get; set; }        
        public string? Description { get; set; }
        public int Duration { get; set; }
        public required string Level { get; set; }



        
        public  ProgramList? ProgramList { get; set; }

        public  Instructor? Instructor { get; set; }
        public ICollection<Member>? Members { get; set; }
        public List<Class>? Classes { get; set; }







    }
}
