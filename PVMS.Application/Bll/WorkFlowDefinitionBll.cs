using Microsoft.EntityFrameworkCore;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.EfContext;
using PVMS.Infrastructure.Extensions;

namespace PVMS.Application.Bll
{
    public class WorkFlowDefinitionBll(
        IBaseDal<WorkFlowDefinition, Guid, WorkFlowDefinitionFilter> baseDal,
        StudioContext context,
        IIdentityManager<Guid> identityManager,
        IInnovaMapper mapper) : BaseBll<WorkFlowDefinition, Guid, WorkFlowDefinitionFilter>(baseDal), IWorkFlowDefinitionBll
    {
        public async Task<(bool ok, string message, Guid id)> SaveNewAsync(WorkFlowDefinitionDto dto)
        {
            var v = Validate(dto, isUpdate: false);
            if (!v.ok) return (false, v.message, default);

            var vr = await ValidateRoleIdsExistAsync(dto);
            if (!vr.ok) return (false, vr.message, default);
            var vt = await ValidateTicketTypeIdsExistAsync(dto);
            if (!vt.ok) return (false, vt.message, default);

            var userId = identityManager.CurrentUserId;
            var now = DateTime.Now;
            var def = new WorkFlowDefinition
            {
                Id = Guid.NewGuid(),
                NameAr = dto.NameAr ?? string.Empty,
                NameOt = dto.NameOt ?? string.Empty,
                Active = dto.Active,
                CreatedBy = userId,
                CreatedDate = now
            };

            var order = 1;
            foreach (var stepDto in dto.Steps)
            {
                var step = new WorkFlowStep
                {
                    Id = Guid.NewGuid(),
                    StepOrder = order++,
                    NameAr = stepDto.NameAr ?? string.Empty,
                    NameOt = stepDto.NameOt ?? string.Empty,
                    RequireNote = stepDto.RequireNote,
                    NotePrompt = stepDto.NotePrompt ?? string.Empty,
                    CreatedBy = userId,
                    CreatedDate = now,
                    WorkFlowDefinition = def
                };
                foreach (var rid in stepDto.ApproveRoleIds ?? [])
                    step.ApproveRoles.Add(new WorkFlowStepApproveRole { Id = Guid.NewGuid(), RoleId = rid, WorkFlowStep = step });
                foreach (var rid in stepDto.RejectRoleIds ?? [])
                    step.RejectRoles.Add(new WorkFlowStepRejectRole { Id = Guid.NewGuid(), RoleId = rid, WorkFlowStep = step });
                foreach (var rid in stepDto.SkipRoleIds ?? [])
                    step.SkipRoles.Add(new WorkFlowStepSkipRole { Id = Guid.NewGuid(), RoleId = rid, WorkFlowStep = step });
                def.Steps.Add(step);
            }
            foreach (var tid in dto.TicketTypeIds ?? [])
                def.TicketTypes.Add(new WorkFlowDefinitionTicketType { Id = Guid.NewGuid(), TicketTypeId = tid, WorkFlowDefinition = def });

            context.WorkFlowDefinitions.Add(def);
            await context.SaveChangesAsync();
            return (true, string.Empty, def.Id);
        }

        public async Task<(bool ok, string message, Guid id)> SaveExistingAsync(WorkFlowDefinitionDto dto)
        {
            var v = Validate(dto, isUpdate: true);
            if (!v.ok) return (false, v.message, default);

            var vr = await ValidateRoleIdsExistAsync(dto);
            if (!vr.ok) return (false, vr.message, default);
            var vt = await ValidateTicketTypeIdsExistAsync(dto);
            if (!vt.ok) return (false, vt.message, default);

            var existing = await context.WorkFlowDefinitions
                .Include(d => d.TicketTypes)
                .Include(d => d.Steps).ThenInclude(s => s.ApproveRoles)
                .Include(d => d.Steps).ThenInclude(s => s.RejectRoles)
                .Include(d => d.Steps).ThenInclude(s => s.SkipRoles)
                .FirstOrDefaultAsync(d => d.Id == dto.Id);
            if (existing == null)
                return (false, "تعريف سير العمل غير موجود.", default);

            foreach (var step in existing.Steps.ToList())
            {
                context.WorkFlowStepApproveRoles.RemoveRange(step.ApproveRoles);
                context.WorkFlowStepRejectRoles.RemoveRange(step.RejectRoles);
                context.WorkFlowStepSkipRoles.RemoveRange(step.SkipRoles);
            }
            context.WorkFlowSteps.RemoveRange(existing.Steps);
            context.WorkFlowDefinitionTicketTypes.RemoveRange(existing.TicketTypes);
            await context.SaveChangesAsync();

            var userId = identityManager.CurrentUserId;
            var now = DateTime.Now;
            existing.NameAr = dto.NameAr ?? string.Empty;
            existing.NameOt = dto.NameOt ?? string.Empty;
            existing.Active = dto.Active;
            existing.ModifiedBy = userId;
            existing.ModifiedDate = now;

            var order = 1;
            foreach (var stepDto in dto.Steps)
            {
                var step = new WorkFlowStep
                {
                    Id = Guid.NewGuid(),
                    WorkFlowDefinitionId = existing.Id,
                    StepOrder = order++,
                    NameAr = stepDto.NameAr ?? string.Empty,
                    NameOt = stepDto.NameOt ?? string.Empty,
                    RequireNote = stepDto.RequireNote,
                    NotePrompt = stepDto.NotePrompt ?? string.Empty,
                    CreatedBy = userId,
                    CreatedDate = now,
                    WorkFlowDefinition = existing
                };
                foreach (var rid in stepDto.ApproveRoleIds ?? [])
                    step.ApproveRoles.Add(new WorkFlowStepApproveRole { Id = Guid.NewGuid(), RoleId = rid, WorkFlowStep = step });
                foreach (var rid in stepDto.RejectRoleIds ?? [])
                    step.RejectRoles.Add(new WorkFlowStepRejectRole { Id = Guid.NewGuid(), RoleId = rid, WorkFlowStep = step });
                foreach (var rid in stepDto.SkipRoleIds ?? [])
                    step.SkipRoles.Add(new WorkFlowStepSkipRole { Id = Guid.NewGuid(), RoleId = rid, WorkFlowStep = step });
                existing.Steps.Add(step);
            }
            foreach (var tid in dto.TicketTypeIds ?? [])
                existing.TicketTypes.Add(new WorkFlowDefinitionTicketType { Id = Guid.NewGuid(), TicketTypeId = tid, WorkFlowDefinition = existing });

            await context.SaveChangesAsync();
            return (true, string.Empty, existing.Id);
        }

        public async Task<WorkFlowDefinitionDto?> GetDetailDtoByIdAsync(Guid id)
        {
            var entity = await context.WorkFlowDefinitions.AsNoTracking()
                .Include(d => d.TicketTypes)
                .Include(d => d.Steps).ThenInclude(s => s.ApproveRoles)
                .Include(d => d.Steps).ThenInclude(s => s.RejectRoles)
                .Include(d => d.Steps).ThenInclude(s => s.SkipRoles)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (entity == null) return null;
            entity.Steps = entity.Steps.OrderBy(x => x.StepOrder).ToList();
            return mapper.Map<WorkFlowDefinitionDto>(entity);
        }

        public async Task<PageResult<WorkFlowDefinitionDto>> GetPagedDtosAsync(WorkFlowDefinitionFilter filter)
        {
            var list = await context.WorkFlowDefinitions.AsNoTracking()
                .Include(d => d.TicketTypes)
                .Include(d => d.Steps).ThenInclude(s => s.ApproveRoles)
                .Include(d => d.Steps).ThenInclude(s => s.RejectRoles)
                .Include(d => d.Steps).ThenInclude(s => s.SkipRoles)
                .ToListAsync();

            foreach (var d in list)
                d.Steps = d.Steps.OrderBy(x => x.StepOrder).ToList();

            IEnumerable<WorkFlowDefinition> q = list;
            if (filter != null)
            {
                if (!filter.Keyword.IsNullOrEmpty())
                    q = q.Where(d =>
                        (d.NameAr?.Contains(filter.Keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (d.NameOt?.Contains(filter.Keyword, StringComparison.OrdinalIgnoreCase) ?? false));
                if (filter.Active.HasValue)
                    q = q.Where(d => d.Active == filter.Active.Value);
                if (filter.Expression != null)
                    q = q.Where(filter.Expression);
            }

            var materialized = q.ToList();
            var page = filter?.PagingParameters?.PageNumber ?? 1;
            var size = filter?.PagingParameters?.PageSize ?? 10;
            var slice = materialized.Skip((page - 1) * size).Take(size).ToList();
            return new PageResult<WorkFlowDefinitionDto>
            {
                Collections = mapper.Map<List<WorkFlowDefinitionDto>>(slice),
                Count = materialized.Count
            };
        }

        public async Task<List<WorkFlowDefinitionMobileDto>> GetMobileAsync()
        {
            var definitions = await context.WorkFlowDefinitions.AsNoTracking()
                .Include(d => d.TicketTypes).ThenInclude(x => x.TicketType)
                .Include(d => d.UserWorkFlowDefinitions)
                .Distinct()
                .ToListAsync();

            return definitions
                .Select(d => new WorkFlowDefinitionMobileDto
                {
                    Id = d.Id,
                    NameAr = d.NameAr,
                    NameOt = d.NameOt,
                    Active = d.Active,
                    TicketTypeIds = d.TicketTypes.Select(x => x.TicketTypeId).Distinct().ToList(),
                    TicketTypes = d.TicketTypes
                        .Where(x => x.TicketType != null)
                        .Select(x => new TicketTypeLookupDto
                        {
                            Id = x.TicketTypeId,
                             NameAr = x.TicketType.NameAr,
                            NameOt = x.TicketType.NameOt,
                            WorkFlowDefinitionId = d.Id,
                            Active = x.TicketType.Active
                        })
                        .GroupBy(x => x.Id)
                        .Select(g => g.First())
                        .ToList(),
                    UserWorkFlowDefinitions = d.UserWorkFlowDefinitions
                        .Select(x => new UserWorkFlowDefinitionDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            WorkFlowDefinitionId = x.WorkFlowDefinitionId
                        })
                        .ToList()
                })
                .ToList();
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await context.WorkFlowDefinitions.FindAsync(id);
            if (entity == null) return false;
            context.WorkFlowDefinitions.Remove(entity);
            return await context.SaveChangesAsync() > 0;
        }

        private static (bool ok, string message) Validate(WorkFlowDefinitionDto dto, bool isUpdate)
        {
            if (dto == null) return (false, "البيانات المرسلة مطلوبة.");
            if (dto.Steps == null || dto.Steps.Count == 0)
                return (false, "يجب إضافة خطوة واحدة على الأقل.");
            for (var i = 0; i < dto.Steps.Count; i++)
            {
                var s = dto.Steps[i];
                if (string.IsNullOrWhiteSpace(s.NameAr) && string.IsNullOrWhiteSpace(s.NameOt))
                    return (false, $"يجب أن تحتوي الخطوة رقم {i + 1} على الاسم العربي أو الاسم الآخر.");
            }
            if (isUpdate && dto.Id == Guid.Empty)
                return (false, "المعرف مطلوب لعملية التعديل.");
            return (true, string.Empty);
        }

        private async Task<(bool ok, string message)> ValidateRoleIdsExistAsync(WorkFlowDefinitionDto dto)
        {
            var allIds = dto.Steps
                .SelectMany(s => (s.ApproveRoleIds ?? []).Concat(s.RejectRoleIds ?? []).Concat(s.SkipRoleIds ?? []))
                .Distinct()
                .ToList();
            if (allIds.Count == 0) return (true, string.Empty);

            var existing = await context.Roles.AsNoTracking().Where(r => allIds.Contains(r.Id)).Select(r => r.Id).ToListAsync();
            var missing = allIds.Except(existing).ToList();
            if (missing.Count > 0)
                return (false, $"معرّفات الأدوار التالية غير موجودة: {string.Join(", ", missing)}");
            return (true, string.Empty);
        }

        private async Task<(bool ok, string message)> ValidateTicketTypeIdsExistAsync(WorkFlowDefinitionDto dto)
        {
            var allIds = (dto.TicketTypeIds ?? []).Distinct().ToList();
            if (allIds.Count == 0) return (true, string.Empty);

            var existing = await context.TicketTypes.AsNoTracking().Where(t => allIds.Contains(t.Id)).Select(t => t.Id).ToListAsync();
            var missing = allIds.Except(existing).ToList();
            if (missing.Count > 0)
                return (false, $"معرّفات أنواع التذاكر التالية غير موجودة: {string.Join(", ", missing)}");
            return (true, string.Empty);
        }
    }
}
