namespace TeamDataForum.Tests.PaginationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Pagination;
    using Pagination.PaginationModels;

    [TestClass]
    public class TestPagination
    {
        [TestMethod]
        public void TestCurrentPageNegative()
        {
            int pageNumber = -1;

            Pagination pagination = new Pagination(pageNumber, 10, 55);

            Assert.AreEqual(1, pagination.CurrentPage);
        }

        [TestMethod]
        public void TestCurrentPageNull()
        {
            int? pageNumber = null;

            Pagination pagination = new Pagination(pageNumber, 10, 55);

            Assert.AreEqual(1, pagination.CurrentPage);
        }

        [TestMethod]
        public void TestSkipTakeFirstPage()
        {
            Pagination pagination = new Pagination(1, 10, 25);

            SkipTake skipTake = pagination.ElementsToSkipAndTake();

            Assert.AreEqual(0, skipTake.Skip);
            Assert.AreEqual(10, skipTake.Take);
            Assert.AreEqual(1, pagination.CurrentPage);
        }

        [TestMethod]
        public void TestSkipTakeSecondPage()
        {
            Pagination pagination = new Pagination(2, 10, 25);

            SkipTake skipTake = pagination.ElementsToSkipAndTake();

            Assert.AreEqual(10, skipTake.Skip);
            Assert.AreEqual(10, skipTake.Take);
            Assert.AreEqual(2, pagination.CurrentPage);
        }

        [TestMethod]
        public void TestSkipTakeThirdPage()
        {
            Pagination pagination = new Pagination(3, 10, 25);

            SkipTake skipTake = pagination.ElementsToSkipAndTake();

            Assert.AreEqual(20, skipTake.Skip);
            Assert.AreEqual(5, skipTake.Take);
            Assert.AreEqual(3, pagination.CurrentPage);
        }

        [TestMethod]
        public void TestSkipTakeInvalidPageNumberShouldReturnFirstForCurrentPage()
        {
            Pagination pagination = new Pagination(5, 10, 25);

            SkipTake skipTake = pagination.ElementsToSkipAndTake();

            Assert.AreEqual(0, skipTake.Skip);
            Assert.AreEqual(10, skipTake.Take);
            Assert.AreEqual(1, pagination.CurrentPage);
        }

        [TestMethod]
        public void TestGetPagesOnlyOnePage()
        {
            Pagination pagination = new Pagination(1, 10, 10);

            IEnumerable<Paginator> pages = pagination.GetPages("action", "controller");

            Assert.AreEqual(1, pages.Count());

            foreach (var page in pages)
            {
                Assert.IsTrue(page.IsCurrentPage);
            }
        }

        [TestMethod]
        public void TestGetPagesTwoPagesCurrentLast()
        {
            Pagination pagination = new Pagination(2, 10, 20);

            IEnumerable<Paginator> pages = pagination.GetPages("action", "controller");

            Assert.AreEqual(2, pages.Count());

            int counter = 0;

            foreach (var page in pages)
            {
                if (counter < 1)
                {
                    Assert.IsFalse(page.IsCurrentPage);
                }
                else
                {
                    Assert.IsTrue(page.IsCurrentPage);
                }

                counter++;  
            }
        }

        [TestMethod]
        public void TestGetPagesTwoPagesCurrentFirst()
        {
            Pagination pagination = new Pagination(1, 10, 20);

            IEnumerable<Paginator> pages = pagination.GetPages("action", "controller");

            Assert.AreEqual(2, pages.Count());

            int counter = 0;

            foreach (var page in pages)
            {
                if (counter < 1)
                {
                    Assert.IsTrue(page.IsCurrentPage);
                }
                else
                {
                    Assert.IsFalse(page.IsCurrentPage);
                }

                counter++;
            }
        }

        [TestMethod]
        public void TestAllPages()
        {
            Pagination pagination = new Pagination(3, 10, 50);

            IEnumerable<Paginator> pages = pagination.GetPages("action", "controller");

            Assert.AreEqual(5, pages.Count());

            int counter = 1;

            foreach (var page in pages)
            {
                if (counter != 3)
                {
                    Assert.IsFalse(page.IsCurrentPage);
                }
                else
                {
                    Assert.IsTrue(page.IsCurrentPage);
                }

                counter++;
            }
        }
    }
}