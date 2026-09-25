using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class KaryawanRepository
    {
        private readonly AzPosDBContext dBContext;
        public KaryawanRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<KaryawanModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            KaryawanModel? karyawan = await dBContext.Karyawan
                .Where(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId))
                .FirstOrDefaultAsync();

            return karyawan;
        }

        public async Task<string> GenerateKodeKaryawan()
        {
            string tenantId = "";
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Karyawan.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeKaryawan = $"{PrefixKodeConstant.Karyawan}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeKaryawan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeKaryawan = $"{PrefixKodeConstant.Karyawan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeKaryawan);
            }

            return kodeKaryawan;
        }        
    }
}