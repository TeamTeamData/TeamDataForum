namespace TeamDataForum.Tests.RepositoryTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DB;
    using DBModels;
    using Repository;
    using Repository.Contracts;

    /// <summary>
    /// Test class for repository
    /// This will create database "TeamDataForumTests" in the local instance of MSSQL.
    /// </summary>
    [TestClass]
    public class TestRepository
    {
        private const string USA = "USA";
        private const string Germany = "Germany";
        private const string Spain = "Spain";

        private readonly string[] CountryNames = { USA, Germany, Spain };

        private IRepository<Country> countryRepository;

        [TestInitialize]
        public void Initialize()
        {
            TeamDataForumContext context = new TeamDataForumContext();

            this.countryRepository = new Repository<Country>(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.countryRepository = null;
        }

        /// <summary>
        /// Test add entity with Repository Add method
        /// </summary>
        [TestMethod]
        public void TestAddCountryToDatabase()
        {
            this.InsertRequiredValues(CountryNames);

            foreach (string name in CountryNames)
            {
                var countries = this.countryRepository.Select(c => c.Name == name);

                Assert.AreEqual(1, countries.Count);
            }
        }

        /// <summary>
        /// Test removing country from database
        /// </summary>
        [TestMethod]
        public void TestRemoveCountryFromDatabase()
        {
            string countryName = USA;

            if (!this.countryRepository.Any(c => c.Name == countryName))
            {
                this.AddCountry(countryName);
            }

            Country usa = this.countryRepository
                .Select(c => c.Name == countryName)
                .FirstOrDefault();

            Assert.AreEqual(countryName, usa.Name);

            this.countryRepository.Remove(usa);

            this.countryRepository.SaveChanges();

            int count = this.countryRepository.Count(c => c.Name == countryName);

            Assert.AreEqual(0, count);
        }

        /// <summary>
        /// test update country
        /// </summary>
        [TestMethod]
        public void TestUpdateCountry()
        {
            string countryName = USA;
            string countryUpdateName = "Romania";

            if (!this.countryRepository.Any(c => c.Name == countryName))
            {
                this.AddCountry(countryName);
            }

            Country usa = this.countryRepository
                .Select(c => c.Name == countryName)
                .First();

            usa.Name = countryUpdateName;
            this.countryRepository.SaveChanges();

            int count = this.countryRepository.Count(c => c.Name == countryUpdateName);

            Assert.AreEqual(1, count);

            usa.Name = countryName;

            this.countryRepository.Update(usa);
            this.countryRepository.SaveChanges();

            int usaCount = this.countryRepository.Count(c => c.Name == countryName);

            Assert.AreEqual(1, usaCount);
        }

        /// <summary>
        /// Test any
        /// </summary>
        [TestMethod]
        public void TestAny()
        {
            bool isGermanyInDB = this.countryRepository.Any(c => c.Name == Germany);

            Assert.IsTrue(isGermanyInDB);

            bool isCanadaInDB = this.countryRepository.Any(c => c.Name == "Canada");

            Assert.IsFalse(isCanadaInDB);
        }

        /// <summary>
        /// Test count 
        /// expected result 3 + 1
        /// one country is added with seeding
        /// </summary>
        [TestMethod]
        public void TestCountNoParameters()
        {
            this.InsertRequiredValues(CountryNames);

            int count = this.countryRepository.Count();

            Assert.AreEqual(4, count);
        }

        /// <summary>
        /// Test count with parameter
        /// </summary>
        [TestMethod]
        public void TestCountWithWhere()
        {
            if (!this.countryRepository.Any(c => c.Name == USA))
            {
                this.AddCountry(USA);
            }

            int count = this.countryRepository.Count(c => c.Name == USA);

            Assert.AreEqual(1, count);
        }

        /// <summary>
        /// Test find
        /// </summary>
        [TestMethod]
        public void TestFind()
        {
            if (!this.countryRepository.Any(c => c.Name == USA))
            {
                this.AddCountry(USA);
            }

            Country usa = this.countryRepository
                .Select(c => c.Name == USA)
                .FirstOrDefault();

            Country usaFinded = this.countryRepository
                .Find(usa.CountryId);

            Assert.AreEqual(USA, usaFinded.Name);
        }

        /// <summary>
        /// Test select
        /// expected 3 + 1
        /// Bulgaria is added with seeding
        /// </summary>
        [TestMethod]
        public void TestSelectNoParameters()
        {
            this.InsertRequiredValues(CountryNames);

            var countries = this.countryRepository.Select();

            Assert.AreEqual(4, countries.Count);
        }

        /// <summary>
        /// Test select with where
        /// </summary>
        [TestMethod]
        public void TestSelectWithWhere()
        {
            this.InsertRequiredValues(CountryNames);

            var countries = this.countryRepository.Select(c => c.Name == USA);

            Assert.AreEqual(1, countries.Count);
        }

        /// <summary>
        /// Test select with where and order by
        /// </summary>
        [TestMethod]
        public void TestSelectWithWhereAndOrderBy()
        {
            this.InsertRequiredValues(CountryNames);

            var countries = this.countryRepository
                .Select(c => c.CountryId > 0, q => q.OrderByDescending(c => c.CountryId));

            for (int i = 0; i < countries.Count - 1; i++)
            {
                Assert.IsTrue(countries[i].CountryId > countries[i + 1].CountryId);
            }
        }

        /// <summary>
        /// Test select with where, orderby, skip and take
        /// </summary>
        [TestMethod]
        public void TestSelectWithWhereAndOrderByAndSkipAndTake()
        {
            this.InsertRequiredValues(CountryNames);

            var countries = this.countryRepository
                .Select(c => c.CountryId > 0, q => q.OrderBy(c => c.CountryId), 2, 2);

            Assert.AreEqual(2, countries.Count);

            for (int i = 0; i < countries.Count - 1; i++)
            {
                Assert.IsTrue(countries[i].CountryId < countries[i + 1].CountryId);
            }
        }

        /// <summary>
        /// Inserts test values
        /// </summary>
        private void InsertRequiredValues(string[] countryNames)
        {
            foreach (string name in countryNames)
            {
                if (!this.countryRepository.Any(c => c.Name == name))
                {
                    this.AddCountry(name);
                }
            }
        }

        /// <summary>
        /// Helper for adding countries
        /// </summary>
        /// <param name="name">Country name</param>
        /// <returns></returns>
        private Country AddCountry(string name)
        {
            Country country = new Country()
            {
                Name = name
            };

            this.countryRepository.Add(country);
            this.countryRepository.SaveChanges();

            return country;
        }
    }
}