using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class TipePelangganRepository
    {
        private readonly AzPosDBContext dBContext;
        public TipePelangganRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<TipePelangganModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            TipePelangganModel? tipePelanggan = await dBContext.TipePelanggan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return tipePelanggan;
        }
        
        public async Task<string> GenerateKodeTipePelanggan()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.TipePelanggan.Where(a => a.TenantId.Equals(tenantId) && a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeTipePelanggan = $"{PrefixKodeConstant.TipePelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodeTipePelanggan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeTipePelanggan = $"{PrefixKodeConstant.TipePelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeTipePelanggan);
            }

            return kodeTipePelanggan;
        }        
    }
}