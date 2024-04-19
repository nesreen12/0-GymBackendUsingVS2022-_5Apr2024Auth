namespace GymBackendUsingVS2022.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public required string UserId { get; set; }
        public required string MemberName { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public DateTime Birthday { get; set; }


        public  User? User { get; set; }
        public  Membership? Membership { get; set; }
        public ICollection<WorkoutPlan>? WorkoutPlans { get; set; }





    }
}
