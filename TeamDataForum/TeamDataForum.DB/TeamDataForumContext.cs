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

        public virtual DbSet<Forum> Forums { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostText> PostTexts { get; set; }

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
                .HasRequired(p => p.Thread)
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

            modelBuilder.Entity<Forum>()
                .HasKey(s => s.ForumId);

            modelBuilder.Entity<Forum>()
                .HasRequired(s => s.Creator)
                .WithMany(u => u.Forums)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(s => s.Moderators)
                .WithMany(u => u.ForumModerators)
                .Map(m =>
                {
                    m.MapLeftKey("SubforumId");
                    m.MapRightKey("UserId");
                    m.ToTable("SubforumsModerators");
                });

            modelBuilder.Entity<Thread>()
                .HasRequired(t => t.Creator)
                .WithMany(u => u.Threads)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}