using VideoConferencingDemo.Infrastructure.Entities.Identity;

namespace VideoConferencingDemo.Infrastructure.Seeds
{
    internal class AdminSeed
    {
        internal ApplicationUser[] Admin
        {
            get
            {
                return new ApplicationUser[]
                {
                    new ApplicationUser{
                        Id = Guid.Parse("95E139BB-6751-4D4B-B14F-12E1597EF982"),
                        UserName = "Admin@gmail.com",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        Email = "Admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = "AQAAAAIAAYagAAAAEFJIs3a7tSdJH9bPoQQgws9S9+h5KK10DZ4Adsyb/IfqBHAAPUidCvxtRsl6V5psxQ==",
                        SecurityStamp = "ELIU5QDSOYTRPKLL64KM2XUMVH2Z3BG2",
                        ConcurrencyStamp = "e0072f2c-38e7-4da8-8f9f-9fdc1949fa69",
                        PhoneNumber = null,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        Image = null,
                        Name = "Admin"
                    }
                };
            }
        }
    }
}
