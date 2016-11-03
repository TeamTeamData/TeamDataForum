namespace TeamDataForum.DB.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DBModels;

    internal sealed class Configuration : DbMigrationsConfiguration<TeamDataForumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TeamDataForumContext context)
        {
            this.CreateTowns(context);
        }

        private void CreateTowns(TeamDataForumContext context)
        {
            string sofia = "Sofia";

            if (!context.Towns.Any(t => t.Name == sofia))
            {
                context.Towns.Add(new Town()
                {
                    Name = sofia,
                    Country = new Country()
                    {
                        Name = "Bulgaria"
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
