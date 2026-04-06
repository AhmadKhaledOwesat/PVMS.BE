using DcpTracker.Application.Interfaces;
using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class FaqBll(IBaseDal<Faq, Guid, FaqFilter> baseDal) : BaseBll<Faq, Guid, FaqFilter>(baseDal), IFaqBll
    {
        public override Task<PageResult<Faq>> GetAllAsync(FaqFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Term.IsNullOrEmpty())
                    searchParameters.Expression = new Func<Faq, bool>(a => a.AnswerAr == searchParameters?.Term && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
