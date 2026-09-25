using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class PelangganRepository
    {
        private readonly AzPosDBContext dBContext;
        public PelangganRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }
        
        public async Task<PelangganModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            PelangganModel? pelanggan = await dBContext.Pelanggan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) &&
                a.TenantId.Equals(tenantId));

            return pelanggan;
        }
        
        public async Task<string> GenerateKodePelanggan()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Pelanggan.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodePelanggan = $"{PrefixKodeConstant.Pelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodePelanggan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodePelanggan = $"{PrefixKodeConstant.Pelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodePelanggan);
            }

            return kodePelanggan;
        }      
    }
}