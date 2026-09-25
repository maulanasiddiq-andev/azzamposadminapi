using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class GudangRepository
    {
        private readonly AzPosDBContext dBContext;
        public GudangRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<GudangModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            GudangModel? gudang = await dBContext.Gudang.
            FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return gudang;
        }

        public async Task<string> GenerateKodeGudang()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Gudang.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeGudang = $"{PrefixKodeConstant.Gudang}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodeGudang);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeGudang = $"{PrefixKodeConstant.Gudang}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeGudang);
            }

            return kodeGudang;
        }        
    }
}