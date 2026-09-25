using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class KurirRepository
    {
        private readonly AzPosDBContext dBContext;
        public KurirRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<KurirModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            KurirModel? kurir = await dBContext.Kurir
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return kurir;
        }
        
        public async Task<string> GenerateKodeKurir()
        {
            string tenantId = "";

            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Kurir.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeKurir = $"{PrefixKodeConstant.Kurir}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeKurir);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeKurir = $"{PrefixKodeConstant.Kurir}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeKurir);
            }

            return kodeKurir;
        }        
    }
}