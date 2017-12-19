using System.Data.Entity.Migrations;

namespace AutoReservation.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<KundenReservationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(KundenReservationContext).FullName;
        }
    }
}
