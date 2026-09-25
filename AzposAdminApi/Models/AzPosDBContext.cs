using AzposAdminApi.Models.Akuntansi;
using AzposAdminApi.Models.Identity;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Models
{
    public class AzPosDBContext : DbContext
    {
        public AzPosDBContext(DbContextOptions<AzPosDBContext> options) : base(options) {}

        #region Masterdata
        public DbSet<TenantModel> Tenant { get; set; }
        public DbSet<TenantConfigModel> TenantConfig { get; set; }
        public DbSet<JabatanModel> Jabatan { get; set; }
        public DbSet<MerekModel> Merek { get; set; }
        public DbSet<JenisModel> Jenis { get; set; }
        public DbSet<SatuanModel> Satuan { get; set; }
        public DbSet<KaryawanModel> Karyawan { get; set; }
        public DbSet<GudangModel> Gudang { get; set; }
        public DbSet<WilayahModel> Wilayah { get; set; }
        public DbSet<GroupPelangganModel> GroupPelanggan { get; set; }
        public DbSet<TipePelangganModel> TipePelanggan { get; set; }
        public DbSet<PelangganModel> Pelanggan { get; set; }
        public DbSet<KurirModel> Kurir { get; set; }
        public DbSet<SupplierModel> Supplier { get; set; }
        public DbSet<MetodePembayaranModel> MetodePembayaran { get; set; }
        #endregion

        #region Akuntansi
        public DbSet<AkunModel> Akun { get; set; }
        public DbSet<MappingAkunModel> MappingAkun { get; set; }
        public DbSet<PajakModel> Pajak { get; set; }
        #endregion

        #region Identity
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<RoleModulModel> RoleModul { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<UserRoleModel> UserRole { get; set; }
        public DbSet<UserNotifikasiModel> UserNotifikasi { get; set; }
        #endregion
    }
}