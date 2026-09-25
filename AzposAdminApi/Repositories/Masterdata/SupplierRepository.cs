using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class SupplierRepository
    {
        private readonly AzPosDBContext dBContext;
        public SupplierRepository(
            AzPosDBContext dBContext
        )
        {
            this.dBContext = dBContext;
        }

        public async Task<SupplierModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            SupplierModel? supplier = await dBContext.Supplier
            .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return supplier;
        }
        
        public async Task<string> GenerateKodeSupplier()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Supplier.Where(a => a.TenantId.Equals(tenantId) && a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeSupplier = $"{PrefixKodeConstant.Supplier}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodeSupplier);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeSupplier = $"{PrefixKodeConstant.Supplier}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeSupplier);
            }

            return kodeSupplier;
        }        
    }
}