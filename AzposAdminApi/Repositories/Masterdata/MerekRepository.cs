using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class MerekRepository
    {
        private readonly AzPosDBContext dBContext;
        public MerekRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<MerekModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            MerekModel? merek = await dBContext.Merek
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return merek;
        }
        
        public async Task<string> GenerateKodeMerek()
        {
            string tenantId = "";
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Merek.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeMerek = $"{PrefixKodeConstant.Merek}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeMerek);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeMerek = $"{PrefixKodeConstant.Merek}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeMerek);
            }

            return kodeMerek;
        }
    }
}