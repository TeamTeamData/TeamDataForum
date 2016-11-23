namespace TeamDataForum.UnitOfWork.Contracts
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using DBModels;
    using Repository;

    /// <summary>
    /// Interface for unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        Repository<Country> CountryRepository { get; }

        Repository<Forum> ForumRepository { get; }

        Repository<Like> LikeRepository { get; }

        Repository<Post> PostRepository { get; }

        Repository<PostText> PostTextRepository { get; }

        Repository<Thread> ThreadRepository { get; }

        Repository<Town> TownRepository { get; }

        Repository<User> UserRepository { get; }

        void SaveChanges();
    }
}
