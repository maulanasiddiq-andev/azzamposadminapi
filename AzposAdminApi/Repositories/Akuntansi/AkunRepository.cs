using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Akuntansi;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Akuntansi
{
    public class AkunRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly IMapper imapper;
        public AkunRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }
        public async Task<IEnumerable<AkunDto>> FindAllActiveAsync()
        {
            string tenantId = "";
            List<AkunModel> akuns = await dBContext.Akun
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.Nama)
                       .ToListAsync();

            List<AkunDto> akunDtos = imapper.Map<List<AkunDto>>(akuns);

            return akunDtos;
        }
    }
}