namespace GymBackendUsingVS2022.Entities
{
    /*public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }*/
    public class Class
    {
        public int ClassId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Location { get; set; }
        public int WorkoutPlanId { get; set; }
        public  WorkoutPlan? WorkoutPlan { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
