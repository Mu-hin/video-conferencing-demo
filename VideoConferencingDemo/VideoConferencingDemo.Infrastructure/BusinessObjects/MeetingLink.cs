namespace VideoConferencingDemo.Infrastructure.BusinessObjects
{
    public class MeetingLink
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public Guid MeetingId { get; set; }
        public DateTime LastUsed { get; set; }
    }
}
