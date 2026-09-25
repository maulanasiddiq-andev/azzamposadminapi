using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class SatuanRepository
    {
        private readonly AzPosDBContext dBContext;
        public SatuanRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<SatuanModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            SatuanModel? satuan = await dBContext.Satuan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return satuan;
        }
        
        public async Task<string> GenerateKodeSatuan()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Satuan.Where(a => a.TenantId.Equals(tenantId) && a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeSatuan = $"{PrefixKodeConstant.Satuan}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodeSatuan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeSatuan = $"{PrefixKodeConstant.Satuan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeSatuan);
            }
            return kodeSatuan;
        }        
    }
}