namespace TeamDataForum.Pagination
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using PaginationModels;

    /// <summary>
    /// Pagination class
    /// </summary>
    public class Pagination : IPagination
    {
        private int currentPage;
        private int elementsToTake;
        private int totalCountElements;

        public Pagination(int? currentPage, int elementsToTake, int totalCountElements)
        {
            this.SetCurrentPage(currentPage);
            this.ElementsToTake = elementsToTake;
            this.TotalCountElements = totalCountElements;
        }

        public int CurrentPage
        {
            get { return this.currentPage; }

            private set { this.currentPage = value; }
        }

        private int ElementsToTake
        {
            get { return this.elementsToTake; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Elements to take is negative number.");
                }

                this.elementsToTake = value;
            }
        }

        private int TotalCountElements
        {
            get { return this.totalCountElements; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Total count elements is negative number.");
                }

                this.totalCountElements = value;
            }
        }

        public SkipTake ElementsToSkipAndTake()
        {
            int elementsToSkip = (this.CurrentPage - 1) * this.ElementsToTake;

            if (elementsToSkip > this.TotalCountElements)
            {
                elementsToSkip = 0;
                this.SetCurrentPage(1);
            }

            int elementsToTakeCount = elementsToSkip + this.ElementsToTake > this.TotalCountElements ?
                this.TotalCountElements - elementsToSkip :
                elementsToTake;

            if (elementsToTakeCount <= 0)
            {
                elementsToTakeCount = this.ElementsToTake;
            }

            return new SkipTake(elementsToSkip, elementsToTakeCount);
        }

        /// <summary>
        /// Returns up to 5 pages for navigation
        /// first, prev, current, next and last
        /// </summary>
        /// <param name="action">Controller action</param>
        /// <param name="controller">Controller</param>
        /// <returns>IEnumerable of Paginator</returns>
        public IEnumerable<Paginator> GetPages(string action, string controller)
        {
            int addition = (this.TotalCountElements % this.ElementsToTake) > 0 ? 
                1 : 0;

            int totalPages = (this.TotalCountElements / this.ElementsToTake) + addition;

            List<Paginator> pages = new List<Paginator>();

            if (this.CurrentPage > 2)
            {
                pages.Add(this.AddPage("|<", action, controller, false));
            }

            if (this.CurrentPage > 1)
            {
                pages.Add(this.AddPage((this.CurrentPage - 1).ToString(), action, controller, false));
            }

            pages.Add(this.AddPage(this.CurrentPage.ToString(), action, controller, true));

            if (this.CurrentPage < totalPages)
            {
                pages.Add(this.AddPage((this.CurrentPage + 1).ToString(), action, controller, false));
            }

            if (this.CurrentPage < totalPages - 1)
            {
                pages.Add(this.AddPage(">|", action, controller, false));
            }

            return pages;
        }

        /// <summary>
        /// Determinates current page
        /// </summary>
        /// <param name="page">current page in controller</param>
        private void SetCurrentPage(int? page)
        {
            this.CurrentPage = page ?? 1;

            if (this.CurrentPage <= 0)
            {
                this.CurrentPage = 1;
            }
        }

        private Paginator AddPage(string pageText, string action, string controller, bool isCurrentPage)
        {
            return new Paginator(action, controller, pageText, isCurrentPage);
        }
    }
}
