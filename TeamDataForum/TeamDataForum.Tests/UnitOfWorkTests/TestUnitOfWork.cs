namespace TeamDataForum.Tests.UnitOfWorkTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DB;
    using DBModels;
    using Repository;
    using UnitOfWork;

    /// <summary>
    /// Test unit of work returns correct repositories
    /// </summary>
    [TestClass]
    public class TestUnitOfWork
    {
        [TestMethod]
        public void TestUnitOfWorkProperties()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new TeamDataForumContext());

            Assert.AreEqual(typeof(Repository<Country>), unitOfWork.CountryRepository.GetType());
            Assert.AreEqual(typeof(Repository<Like>), unitOfWork.LikeRepository.GetType());
            Assert.AreEqual(typeof(Repository<Post>), unitOfWork.PostRepository.GetType());
            Assert.AreEqual(typeof(Repository<PostText>), unitOfWork.PostTextRepository.GetType());
            Assert.AreEqual(typeof(Repository<Subforum>), unitOfWork.SubforumRepository.GetType());
            Assert.AreEqual(typeof(Repository<Thread>), unitOfWork.ThreadRepository.GetType());
            Assert.AreEqual(typeof(Repository<Town>), unitOfWork.TownRepository.GetType());
            Assert.AreEqual(typeof(Repository<User>), unitOfWork.UserRepository.GetType());
        }
    }
}
