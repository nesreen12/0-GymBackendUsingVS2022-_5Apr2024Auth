namespace GymBackendUsingVS2022.Entities
{
    public class Membership
    {
        public int MembershipId { get; set; }
        public DateTime JoinDate { get; set;}
        public DateTime ExpireyDate { get; set;}
        public int MembershipTypeId { get; set; }
        public  MembershipType? MembershipType { get; set; }
        public int MemberId { get; set; }
        public  Member? Member { get; set; }
    }
}
