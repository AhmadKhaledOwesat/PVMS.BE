using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class WorkFlowHistoryBll(IBaseDal<WorkFlowHistory, Guid, WorkFlowHistoryFilter> baseDal) : BaseBll<WorkFlowHistory, Guid, WorkFlowHistoryFilter>(baseDal), IWorkFlowHistoryBll
    {
        public override Task<PageResult<WorkFlowHistory>> GetAllAsync(WorkFlowHistoryFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                searchParameters.Expression = new Func<WorkFlowHistory, bool>(a =>
                a.TicketId == searchParameters.TicketId
                );
            }

            return base.GetAllAsync(searchParameters);
        }

        public override Task AddAsync(WorkFlowHistory entity)
        {
            entity.Title = $"{GetStatus(entity.NewStatusId)}";
            entity.Title = $"{entity.Title} {(entity.IsSkip ? "/ تم التخطي" : "")}";
            return base.AddAsync(entity);
        }

        private static string GetStatus(int statusId)
        {
            string description = "";
            switch (statusId)
            {

                case -1:
                    description = "تم الاعادة للميدان";
                    break;
                case 1:
                    description = "تم استلام الضبط من الميدان";
                    break;
                case 2:
                    description = "تمت المراجعة من قبل المشرف";
                    break;
                case 3:
                    description = "تم التحقق من قبل مدير العمليات والسيطرة";
                    break;
                case 4:
                    description = "تمت المراجعة من الجهة القانونية";
                    break;
                case 5:
                    description = "تم التحويل للمحكمة";
                    break;
                case 6:
                    description = "مؤرشف";
                    break;
                default:
                    break;
            }
            return description;
        }
    }
}
