namespace TeamDataForum.Web.Controllers.Bases
{
    using System;
    using Pagination.Contracts;
    using UnitOfWork.Contracts;

    public class ForumPageBaseController : ForumBaseController
    {
        private const string PaginationIsNull = "Paginator factory is null.";

        private IPaginationFactory paginationFactory;

        public ForumPageBaseController(IUnitOfWork unitOfWork, IPaginationFactory paginationFactory) 
            : base(unitOfWork)
        {
            this.PaginationFactory = paginationFactory;
        }

        protected IPaginationFactory PaginationFactory
        {
            get { return this.paginationFactory; }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(PaginationIsNull);
                }

                this.paginationFactory = value;
            }
        }
    }
}