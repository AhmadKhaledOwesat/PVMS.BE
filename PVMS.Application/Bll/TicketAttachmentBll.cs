using Castle.Core.Configuration;
using Microsoft.Extensions.Options;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class TicketAttachmentBll(IBaseDal<TicketAttachment, Guid, TicketAttachmentFilter> baseDal, IOptions<AssetsOptions> configuration) : BaseBll<TicketAttachment, Guid, TicketAttachmentFilter>(baseDal), ITicketAttachmentBll
    {
        public override async Task<PageResult<TicketAttachment>> GetAllAsync(TicketAttachmentFilter searchParameters)
        {
            string path = Path.Combine(configuration.Value.Path, searchParameters.TicketId.ToString(), "uploads");
            if (!Directory.Exists(path))
                return new PageResult<TicketAttachment>();
            var files = Directory.GetFiles(path);
            var result = new PageResult<TicketAttachment>();
            foreach (var file in files)
            {
                result.Collections.Add(new TicketAttachment { Path = file.Replace(configuration.Value.BasePath, "") });
            }
            return await Task.FromResult(result);
        }

    }
}
