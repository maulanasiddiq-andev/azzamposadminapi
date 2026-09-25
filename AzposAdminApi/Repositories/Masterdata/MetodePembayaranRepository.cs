using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class MetodePembayaranRepository
    {
        private readonly AzPosDBContext dBContext;

        public MetodePembayaranRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<MetodePembayaranModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            MetodePembayaranModel? metodePembayaran = await dBContext.MetodePembayaran
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return metodePembayaran;
        }
        
        public async Task<string> GenerateKodeMetodePembayaran()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.MetodePembayaran.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeMetodePembayaran = $"{PrefixKodeConstant.MetodePembayaran}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeMetodePembayaran);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeMetodePembayaran = $"{PrefixKodeConstant.MetodePembayaran}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeMetodePembayaran);
            }

            return kodeMetodePembayaran;
        }        
    }
}