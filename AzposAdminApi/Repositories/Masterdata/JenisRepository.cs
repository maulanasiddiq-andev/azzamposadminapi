using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class JenisRepository
    {
        private readonly AzPosDBContext dBContext;
        public JenisRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }
        
        public async Task<JenisModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            JenisModel? jenis = await dBContext.Jenis
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return jenis;
        }
        
        public async Task<string> GenerateKodeJenis()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Jenis.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeJenis = $"{PrefixKodeConstant.Jenis}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeJenis);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeJenis = $"{PrefixKodeConstant.Jenis}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeJenis);
            }

            return kodeJenis;
        }        
    }
}