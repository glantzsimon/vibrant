
using K9.Base.DataAccessLayer.Database;
using K9.DataAccessLayer.Database;
using K9.DataAccessLayer.Database.Seeds;
using K9.WebApplication.Config;
using WebMatrix.WebData;

namespace K9.WebApplication
{
    public class DataConfig
    {
        public static void InitialiseDatabase(SeedConfiguration config)
        {
            //var migrator = new DbMigrator(new DatabaseInitialiserLocal(config.EnableSeed));
            //migrator.Update();
        }

        public static void InitialiseWebsecurity()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "User", "Id", "UserName", true);
            }
        }

        public static void InitialiseUsersAndRoles(SeedConfiguration config)
        {
            if (config.EnableSeed)
            {
                UsersAndRolesInitialiser.Seed();
                PermissionsSeeder.Seed(new LocalDb());
            }
        }
    }
}