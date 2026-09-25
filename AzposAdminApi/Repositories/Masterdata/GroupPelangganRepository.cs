using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class GroupPelangganRepository
    {
        private readonly AzPosDBContext dBContext;
        public GroupPelangganRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }
        
        public async Task<GroupPelangganModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            GroupPelangganModel? groupPelanggan = await dBContext.GroupPelanggan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId) && a.RecordStatus.Equals(RecordStatusConstant.Active));

            return groupPelanggan;
        }
        
        public async Task<string> GenerateKodeGroupPelanggan()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.GroupPelanggan.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeGroupPelanggan = $"{PrefixKodeConstant.GroupPelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeGroupPelanggan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeGroupPelanggan = $"{PrefixKodeConstant.GroupPelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeGroupPelanggan);
            }

            return kodeGroupPelanggan;
        }        
    }
}