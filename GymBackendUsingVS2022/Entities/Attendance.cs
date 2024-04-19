namespace GymBackendUsingVS2022.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public required string UserId { get; set; }
        public int ClassId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }

        public User? User { get; set; }
        public Class? Class { get; set; }
    }
}
