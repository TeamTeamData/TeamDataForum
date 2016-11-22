namespace TeamDataForum.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using DB;
    using DBModels;
    using Repository;

    /// <summary>
    /// Combines all repositories into one unit of work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private const string ContextNullError = "Context cannot be null.";

        private readonly Dictionary<Type, object> repositoriesByType;

        private TeamDataForumContext context;

        public UnitOfWork(TeamDataForumContext context)
        {
            this.Context = context;
            this.repositoriesByType = new Dictionary<Type, object>();
        }

        private TeamDataForumContext Context
        {
            get { return this.context; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(ContextNullError);
                }

                this.context = value;
            }
        }

        public Repository<Country> CountryRepository
        {
            get { return this.GetRepository<Country>(); }
        }

        public Repository<Like> LikeRepository
        {
            get { return this.GetRepository<Like>(); }
        }

        public Repository<Post> PostRepository
        {
            get { return this.GetRepository<Post>(); }
        }

        public Repository<PostText> PostTextRepository
        {
            get { return this.GetRepository<PostText>(); }
        }

        public Repository<Forum> ForumRepository
        {
            get { return this.GetRepository<Forum>(); }
        }

        public Repository<Thread> ThreadRepository
        {
            get { return this.GetRepository<Thread>(); }
        }

        public Repository<Town> TownRepository
        {
            get { return this.GetRepository<Town>(); }
        }

        public Repository<User> UserRepository
        {
            get { return this.GetRepository<User>(); }
        }

        private Repository<T> GetRepository<T>()
            where T : class
        {
            object repository;

            if (this.repositoriesByType.TryGetValue(typeof(T), out repository))
            {
                return (Repository<T>)repository;
            }

            return this.AddRepositoryToDictionary<T>();
        }

        private Repository<T> AddRepositoryToDictionary<T>()
            where T : class
        {
            Repository<T> newRepository = new Repository<T>(this.Context);

            this.repositoriesByType.Add(typeof(T), newRepository);

            return newRepository;
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
