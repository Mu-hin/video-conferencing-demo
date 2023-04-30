namespace VideoConferencingDemo.Infrastructure.Services
{
    public interface ITimeService
    {
        public DateTime Now { get; }
        public DateTime Parse(string date);
        public DateTime ParseDayEnd(string date);
    }
}
