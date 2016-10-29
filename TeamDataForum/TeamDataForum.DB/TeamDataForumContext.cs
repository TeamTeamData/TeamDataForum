namespace TeamDataForum.DB
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DBModels;

    /// <summary>
    /// Forum context
    /// </summary>
    public class TeamDataForumContext : IdentityDbContext<User>
    {
        public TeamDataForumContext()
            : base("name=TeamDataForumContext")
        {
        }

        /// <summary>
        /// Sets
        /// </summary>
        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostText> PostTexts { get; set; }

        public virtual DbSet<Subforum> Subforums { get; set; }   

        public virtual DbSet<Topic> Topics { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

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
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Like>()
                .HasRequired(l => l.Post)
                .WithMany(p => p.Likes)
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

            base.OnModelCreating(modelBuilder);
        }
    }
}