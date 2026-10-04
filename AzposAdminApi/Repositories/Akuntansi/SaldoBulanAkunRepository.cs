using AzposAdminApi.Models;
using AzposAdminApi.Models.Akuntansi;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Akuntansi
{
    public class SaldoBulanAkunRepository
    {
        private readonly AzPosDBContext dbContext;
        public SaldoBulanAkunRepository(
            AzPosDBContext dbContext
        )
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SaldoBulananAkunModel>> FindSaldoByAkunIdAsync(string akunId, string tenantId)
        {
            List<SaldoBulananAkunModel> saldoBulananAkuns = await dbContext
                .SaldoBulananAkun
                .Include(a => a.Akun)
                .Where(a => a.AkunId.Equals(akunId) && a.TenantId.Equals(tenantId))
                .OrderByDescending(a => a.Tanggal)
                .ToListAsync();

            return saldoBulananAkuns;
        }

        public async Task<decimal> FindLastSaldoByAkunIdAsync(string akunId)
        {
            decimal lastSaldo = await dbContext
                .SaldoBulananAkun
                .Where(a => a.AkunId.Equals(akunId))
                .OrderByDescending(a => a.Tanggal)
                .Select(a => a.Saldo)
                .FirstOrDefaultAsync();

            return lastSaldo;
        }
    }
}