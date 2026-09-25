using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Akuntansi;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Akuntansi
{
    public class MappingAkunRepository
    {
        private readonly IMapper imapper;
        private readonly AzPosDBContext dBContext;
        public MappingAkunRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }

        public async Task<IEnumerable<MappingAkunDto>> FindAllActiveAsync()
        {
            string tenantId = "";
            List<MappingAkunModel> mappingAkuns = await dBContext.MappingAkun
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.MappingConstant)
                       .ToListAsync();

            List<MappingAkunDto> mappingAkunDtos = imapper.Map<List<MappingAkunDto>>(mappingAkuns);

            return mappingAkunDtos;
        }        
    }
}