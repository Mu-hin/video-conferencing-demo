namespace VideoConferencingDemo.Infrastructure.Services
{
    public class TimeService : ITimeService
    {
        public DateTime Now
        {
            get => DateTime.UtcNow;
        }

        public DateTime Parse(string date)
        {
            return DateTime.Parse(date);
        }

        public DateTime ParseDayEnd(string date)
        {
            var dayEnd = DateTime.Parse(date);

            return new DateTime(dayEnd.Year, 
                dayEnd.Month, 
                dayEnd.Day, 
                23, 59, 59, 999);
        }
    }
}
