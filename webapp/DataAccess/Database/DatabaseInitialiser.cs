using K9.Base.DataAccessLayer.Database;
using K9.DataAccessLayer.Database.Seeds;

namespace K9.DataAccessLayer.Database
{
    public class DatabaseInitialiserLocal : DatabaseInitialiser<LocalDb>
	{
	    public bool EnableSeeed { get; set; }
       
	    public DatabaseInitialiserLocal()
	    {}

	    public DatabaseInitialiserLocal(bool enableSeed)
	    {
	        EnableSeeed = enableSeed;
	    }

        protected override void Seed(LocalDb db)
        {
            if (EnableSeeed)
            {
                base.Seed(db);
                MembershipOptionsSeeder.Seed(db);
            }
        }
	}
}
