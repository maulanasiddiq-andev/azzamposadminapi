using AzposAdminApi.Repositories.Akuntansi;
using AzposAdminApi.Repositories.Identity;
using AzposAdminApi.Repositories.Masterdata;
using AzposAdminApi.Services;

namespace AzposAdminApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterRepositories(this IServiceCollection collection)
        {
            #region  Akuntansi
            collection.AddTransient<AkunRepository>();
            collection.AddTransient<MappingAkunRepository>();
            collection.AddTransient<PajakRepository>();
            #endregion

            #region Masterdata
            collection.AddTransient<TenantRepository>();
            collection.AddTransient<GroupPelangganRepository>();
            collection.AddTransient<GudangRepository>();
            collection.AddTransient<JabatanRepository>();
            collection.AddTransient<JenisRepository>();
            collection.AddTransient<KaryawanRepository>();
            collection.AddTransient<KurirRepository>();
            collection.AddTransient<MerekRepository>();
            collection.AddTransient<MetodePembayaranRepository>();
            collection.AddTransient<PelangganRepository>();
            collection.AddTransient<SatuanRepository>();
            collection.AddTransient<SupplierRepository>();
            collection.AddTransient<TenantRepository>();
            collection.AddTransient<TipePelangganRepository>();
            #endregion

            #region Identity
            collection.AddTransient<UserRepository>();
            collection.AddTransient<RoleRepository>();
            #endregion

            #region Services
            collection.AddTransient<ActivityLogService>();
            #endregion
        }
    }
}