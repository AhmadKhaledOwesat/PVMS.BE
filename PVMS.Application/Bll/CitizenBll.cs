using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class CitizenBll(IBaseDal<Citizen, Guid, CitizenFilter> baseDal) : BaseBll<Citizen, Guid, CitizenFilter>(baseDal), ICitizenBll
    {
        public async Task<List<string>> GetCitizenImageFileNamesAsync()
        {
            var citizens = await baseDal.FindAllByExpressionAsync(c => c.ImagePath != null && c.ImagePath != "");
            return [.. citizens.Select(c => c.ImagePath!)];
        }
    }
}
