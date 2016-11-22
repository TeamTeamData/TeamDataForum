namespace TeamDataForum.Web.Controllers.Bases
{
    using System;
    using Pagination.Contracts;
    using UnitOfWork.Contracts;

    public class ForumPageBaseController : ForumBaseController
    {


            : base(unitOfWork)
        {
        }

        {

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(PaginationIsNull);
                }

            }
        }
    }
}
