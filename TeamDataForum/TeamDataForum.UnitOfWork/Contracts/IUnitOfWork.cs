﻿namespace TeamDataForum.UnitOfWork.Contracts
{
    using DBModels;
    using Repository;

    /// <summary>
    /// Interface for unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        Repository<Country> CountryRepository { get; }

        Repository<Like> LikeRepository { get; }

        Repository<Post> PostRepository { get; }

        Repository<PostText> PostTextRepository { get; }

        Repository<Forum> ForumRepository { get; }

        Repository<Thread> ThreadRepository { get; }

        Repository<Town> TownRepository { get; }

        Repository<User> UserRepository { get; }

        void SaveChanges();
    }
}
