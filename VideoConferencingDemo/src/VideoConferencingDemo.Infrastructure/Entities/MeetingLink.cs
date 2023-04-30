namespace VideoConferencingDemo.Infrastructure.Entities
{
    public class MeetingLink : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public Guid MeetingId { get; set; }
        public DateTime LastUsed { get; set; }
    }
}
