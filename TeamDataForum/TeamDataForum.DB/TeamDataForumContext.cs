namespace TeamDataForum.DB
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DBModels;
    using Migrations;

    /// <summary>
    /// Forum context
    /// </summary>
    public class TeamDataForumContext : IdentityDbContext<User>
    {
        public TeamDataForumContext()
            : base("name=TeamDataForumContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TeamDataForumContext, Configuration>());
        }

        /// <summary>
        /// Sets
        /// </summary>
        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostText> PostTexts { get; set; }

        public virtual DbSet<Subforum> Subforums { get; set; }   

        public virtual DbSet<Thread> Topics { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

        public static TeamDataForumContext Create()
        {
            return new TeamDataForumContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasKey(c => c.CountryId);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Towns)
                .WithRequired(t => t.Country);

            modelBuilder.Entity<Like>()
                .HasKey(l => l.LikeId);

            modelBuilder.Entity<Like>()
                .HasRequired(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Like>()
                .HasRequired(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Post>()
                .HasRequired(p => p.Creator)
                .WithMany(u => u.Posts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasRequired(p => p.Topic)
                .WithMany(t => t.Posts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasOptional(p => p.ResponseTo)
                .WithMany(p => p.Responses)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasRequired(p => p.Text)
                .WithRequiredDependent(pt => pt.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subforum>()
                .HasKey(s => s.SubforumId);

            modelBuilder.Entity<Subforum>()
                .HasRequired(s => s.Creator)
                .WithMany(u => u.Subforums)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subforum>()
                .HasMany(s => s.Moderators)
                .WithMany(u => u.SubforumsModerator)
                .Map(m =>
                {
                    m.MapLeftKey("SubforumId");
                    m.MapRightKey("UserId");
                    m.ToTable("SubforumsModerators");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}