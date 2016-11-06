namespace TeamDataForum.UnitOfWork.Contracts
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

        Repository<Subforum> SubforumRepository { get; }

        Repository<Thread> TopicRepository { get; }

        Repository<Town> TownRespository { get; }

        Repository<User> UserRepository { get; }
    }
}
