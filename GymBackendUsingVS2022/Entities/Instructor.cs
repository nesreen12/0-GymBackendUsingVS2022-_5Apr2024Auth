namespace GymBackendUsingVS2022.Entities
{
    public class Instructor 
    {
        public int InstructorId { get; set; }
        public required string UserId { get; set; }
        public required string Name { get; set; }        
        public string? Specialization { get; set; }
        public string? Certification { get; set; }

        
        public  User? User { get; set; }
        public List<WorkoutPlan>? WorkoutPlans { get; set; }




    }
}
