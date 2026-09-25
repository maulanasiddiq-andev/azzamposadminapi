using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Akuntansi;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Akuntansi
{
    public class PajakRepository
    {
        private readonly IMapper imapper;
        private readonly AzPosDBContext dBContext;
        public PajakRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }

        public async Task<IEnumerable<PajakDto>> FindAllActiveAsync()
        {
            string tenantId = "";
            List<PajakModel> pajaks = await dBContext.Pajak
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.Nama)
                       .ToListAsync();

            List<PajakDto> pajakDtos = imapper.Map<List<PajakDto>>(pajaks);

            return pajakDtos;
        }
    }
}