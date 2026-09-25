using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class JabatanRepository
    {
        private readonly AzPosDBContext dBContext;
        public JabatanRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<JabatanModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            JabatanModel? jabatan = await dBContext.Jabatan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return jabatan;
        }
        
        public async Task<string> GenerateKodeJabatan()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Jabatan.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeJabatan = $"{PrefixKodeConstant.Jabatan}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeJabatan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeJabatan = $"{PrefixKodeConstant.Jabatan}{dateNow:MM}{dateNow:yy}{totalItem}";

                existNomor = await FindByKodeAsync(kodeJabatan);
            }

            return kodeJabatan;
        }        
    }
}