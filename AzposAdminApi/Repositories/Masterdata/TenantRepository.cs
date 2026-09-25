using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Exceptions;
using AzposAdminApi.Extensions;
using AzposAdminApi.Helpers;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Akuntansi;
using AzposAdminApi.Models.Masterdata;
using AzposAdminApi.Repositories.Akuntansi;
using AzposAdminApi.Repositories.Identity;
using AzposAdminApi.Services;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class TenantRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly string tableName = "Tenant";
        private readonly string userId = "";
        private readonly string tenantId = "";
        private readonly ActivityLogService activityLogService;
        private readonly ActionModelHelper actionModelHelper;
        private readonly IMapper imapper;
        private readonly PajakRepository pajakRepository;
        private readonly AkunRepository akunRepository;
        private readonly MappingAkunRepository mappingAkunRepository;
        private readonly MetodePembayaranRepository metodePembayaranRepository;
        private readonly KurirRepository kurirRepository;
        private readonly GroupPelangganRepository groupPelangganRepository;
        private readonly TipePelangganRepository tipePelangganRepository;
        private readonly JabatanRepository jabatanRepository;
        private readonly GudangRepository gudangRepository;
        private readonly KaryawanRepository karyawanRepository;
        private readonly PelangganRepository pelangganRepository;
        private readonly SatuanRepository satuanRepository;
        private readonly JenisRepository jenisRepository;
        private readonly MerekRepository merekRepository;
        private readonly SupplierRepository supplierRepository;
        private readonly UserRepository userRepository;
        public TenantRepository(
            AzPosDBContext dBContext,
            ActivityLogService activityLogService,
            IMapper imapper,
            PajakRepository pajakRepository,
            AkunRepository akunRepository,
            MappingAkunRepository mappingAkunRepository,
            MetodePembayaranRepository metodePembayaranRepository,
            KurirRepository kurirRepository,
            GroupPelangganRepository groupPelangganRepository,
            TipePelangganRepository tipePelangganRepository,
            JabatanRepository jabatanRepository,
            GudangRepository gudangRepository,
            KaryawanRepository karyawanRepository,
            PelangganRepository pelangganRepository,
            SatuanRepository satuanRepository,
            JenisRepository jenisRepository,
            MerekRepository merekRepository,
            SupplierRepository supplierRepository,
            UserRepository userRepository
        )
        {
            this.dBContext = dBContext;
            this.activityLogService = activityLogService;
            this.pajakRepository = pajakRepository;
            this.akunRepository = akunRepository;
            this.mappingAkunRepository = mappingAkunRepository;
            this.metodePembayaranRepository = metodePembayaranRepository;
            this.kurirRepository = kurirRepository;
            this.groupPelangganRepository = groupPelangganRepository;
            this.tipePelangganRepository = tipePelangganRepository;
            this.jabatanRepository = jabatanRepository;
            this.gudangRepository = gudangRepository;
            this.karyawanRepository = karyawanRepository;
            this.pelangganRepository = pelangganRepository;
            this.satuanRepository = satuanRepository;
            this.jenisRepository = jenisRepository;
            this.merekRepository = merekRepository;
            this.supplierRepository = supplierRepository;
            this.userRepository = userRepository;

            this.imapper = imapper;

            actionModelHelper = new ActionModelHelper();
        }

        public async Task CreateAsync(TenantModel tenant)
        {
            if (tenant is null)
            {
                throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Tenant"));
            }

            if (tenant is null)
            {
                throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Tenant"));
            }

            using var transaction = dBContext.Database.BeginTransaction();
            try
            {
                string createdBy = userId ?? "Generator";

                actionModelHelper.AssignCreateModel(tenant, tableName, createdBy, null);

                TenantConfigModel tenantConfig = new TenantConfigModel()
                {
                    TenantId = tenant.TenantId,
                    UkuranKertasAlamatPengiriman = UkuranKertasPrintContant.A4,
                    UkuranKertasPesananPenjualan = UkuranKertasPrintContant.A4,
                    UkuranKertasLabelPengiriman = UkuranKertasPrintContant.A4,
                    UkuranKertasInvoicePenjualan = UkuranKertasPrintContant.A4,
                    RecordStatus = RecordStatusConstant.Active,
                    Deskripsi = "Default"
                };

                actionModelHelper.AssignCreateModel(tenantConfig, "TenantConfig", createdBy, null);

                tenant.Kode = await GenerateKodeTenant();
                dBContext.Add(tenant);
                dBContext.Add(tenantConfig);
                await dBContext.SaveChangesAsync();

                // Add Akun
                DateTime dateNow = DateTime.UtcNow;
                var listPajak = await pajakRepository.FindAllActiveAsync();

                int totalItem = 0;
                foreach (var item in listPajak)
                {
                    item.OldId = item.PajakId;
                    item.Kode = $"{PrefixKodeConstant.Pajak}{dateNow:MM}{dateNow:yy}{totalItem}";
                    item.PajakId = Guid.NewGuid().ToString("N");

                    totalItem++;
                }

                var listAkun = await akunRepository.FindAllActiveAsync();
                var listMappingAkun = await mappingAkunRepository.FindAllActiveAsync();

                listAkun = listAkun.OrderBy(a => a.NomorAkun).ToList();

                foreach (var item in listAkun)
                {
                    string newId = Guid.NewGuid().ToString("N");
                    item.Saldo = 0;
                    item.PajakId = listPajak.FirstOrDefault(a => a.OldId == item.PajakId)?.PajakId ?? "";
                    item.OldId = item.AkunId;
                    item.AkunId = newId;
                    item.CoaId = item.CoaId;
                }

                foreach (var item in listAkun)
                {
                    item.ChildOfAkunId = listAkun.FirstOrDefault(a => a.OldId == item.ChildOfAkunId)?.AkunId ?? "";
                }

                totalItem = 0;
                foreach (var item in listMappingAkun)
                {
                    string newId = Guid.NewGuid().ToString("N");

                    item.Kode = $"{PrefixKodeConstant.MappingAkun}{dateNow:MM}{dateNow:yy}{totalItem}";
                    item.MappingAkunId = newId;
                    item.AkunId = listAkun.FirstOrDefault(a => a.OldId == item.AkunId)?.AkunId ?? "";

                    totalItem++;
                }

                foreach (var pajak in listPajak)
                {
                    var newPajak = imapper.Map<PajakModel>(pajak);
                    actionModelHelper.AssignCreateWithExistingId(newPajak, "Pajak", pajak.PajakId, tenant.CreatedBy, tenant.TenantId);

                    dBContext.Add(newPajak);
                    await dBContext.SaveChangesAsync();
                }

                foreach (var akun in listAkun)
                {
                    var newAkun = imapper.Map<AkunModel>(akun);
                    actionModelHelper.AssignCreateWithExistingId(newAkun, "Akun", akun.AkunId, tenant.CreatedBy, tenant.TenantId);

                    dBContext.Add(newAkun);
                    await dBContext.SaveChangesAsync();
                }

                foreach (var mapping in listMappingAkun)
                {
                    var newMapping = imapper.Map<MappingAkunModel>(mapping);
                    actionModelHelper.AssignCreateWithExistingId(newMapping, "MappingAkun", mapping.MappingAkunId, tenant.CreatedBy, tenant.TenantId);

                    dBContext.Add(newMapping);
                    await dBContext.SaveChangesAsync();
                }

                // Add Admin
                DefaultGeneratorHelper generatorHelper = new(tenant.CreatedBy, tenant.TenantId, dBContext);
                await generatorHelper.GenerateFirstAdmin(tenant.Nama);

                await dBContext.SaveChangesAsync();
                await transaction.CommitAsync();

                await GenerateDefaultData(tenant, tenantId);
            }
            catch (KnownException ex)
            {
                await transaction.RollbackAsync();

                throw new KnownException(ex.Message);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                activityLogService.SaveErrorLog(ex, "Create Tenant", userId, tenantId);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TenantDto>> FindAllActiveAsync()
        {
            List<TenantModel> listTenant = await dBContext.Tenant
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active))
                       .OrderBy(a => a.Nama)
                       .ToListAsync();

            List<TenantDto> listTenantDto = imapper.Map<List<TenantDto>>(listTenant);

            return listTenantDto;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(SearchSortFilterPagingDto searchSortFilterPaging)
        {
            IQueryable<TenantModel> listTenantQuery = dBContext.Tenant
                .AsQueryable();

            #region Filtering
            string? recordStatus = searchSortFilterPaging.RecordStatus;

            if (string.IsNullOrEmpty(recordStatus))
            {
                listTenantQuery = listTenantQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listTenantQuery = listTenantQuery
                    .Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) ||
                a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listTenantQuery = listTenantQuery.Where(a => a.RecordStatus.ToLower().Equals(recordStatus.ToLower())).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchSortFilterPaging.Search))
            {
                string search = searchSortFilterPaging.Search.ToLower();

                listTenantQuery = listTenantQuery.Where(a => a.Nama.ToLower().Contains(search)).AsQueryable();
            }
            #endregion

            #region ordering

            string? sortBy = searchSortFilterPaging.SortBy;
            string? sortDir = searchSortFilterPaging.SortDir;

            // Set default ordering
            if (string.IsNullOrEmpty(sortDir))
                sortDir = "asc";

            if (string.IsNullOrEmpty(sortBy))
                sortBy = "nama";

            sortDir = sortDir.ToLower();
            sortBy = sortBy.ToLower();

            switch (sortBy)
            {
                case "nama":
                    if (sortDir == "asc")
                    {
                        listTenantQuery = listTenantQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.TenantId).AsQueryable();
                    }
                    else
                    {
                        listTenantQuery = listTenantQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.TenantId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listTenantQuery = listTenantQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.TenantId).AsQueryable();
                    }
                    else
                    {
                        listTenantQuery = listTenantQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.TenantId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listTenantQuery = listTenantQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.TenantId).AsQueryable();
                    }
                    else
                    {
                        listTenantQuery = listTenantQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.TenantId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchSortFilterPaging.IsCount)
            {
                totalItem = await listTenantQuery.CountAsync();
            }

            #endregion

            #region mappingObject
            int skip = searchSortFilterPaging.PageIndex * searchSortFilterPaging.PageSize;
            int take = searchSortFilterPaging.PageSize;

            List<TenantModel> listTenant = await listTenantQuery.Skip(skip).Take(take).ToListAsync();

            List<TenantDto> listTenantDto = imapper.Map<List<TenantDto>>(listTenant);

            SearchSortPagingResponseDto objectResult = new()
            {
                Items = listTenantDto,
                TotalItem = totalItem,
                CurrentPage = searchSortFilterPaging.PageIndex,
                PageSize = searchSortFilterPaging.PageSize
            };

            #endregion

            return objectResult;
        }

        public async Task<TenantDto> FindByIdWithTenantConfigAsync(string tenantId)
        {
            TenantModel? tenant = await dBContext.Tenant.SingleOrDefaultAsync(a => a.TenantId.Equals(tenantId));
            var tenantDto = imapper.Map<TenantDto>(tenant);

            if (!string.IsNullOrEmpty(tenant?.Timezone))
            {
                var timezoneinfo = TimeZoneInfo.FindSystemTimeZoneById(tenant.Timezone);

                tenantDto.TimezoneName = timezoneinfo.DisplayName;
            }

            TenantConfigModel? tenantConfig = await FindTenantConfigByTenantIdAsync(tenantId);

            if (tenantConfig != null)
            {
                tenantDto.BotUserId = tenantConfig.BotUserId;
                tenantDto.UkuranKertasAlamatPengiriman = tenantConfig.UkuranKertasAlamatPengiriman;
                tenantDto.UkuranKertasInvoicePenjualan = tenantConfig.UkuranKertasInvoicePenjualan;
                tenantDto.UkuranKertasLabelPengiriman = tenantConfig.UkuranKertasLabelPengiriman;
                tenantDto.UkuranKertasPesananPenjualan = tenantConfig.UkuranKertasPesananPenjualan;

                if (!string.IsNullOrEmpty(tenantConfig.BotUserId))
                {
                    var user = await userRepository.FindByIdAsync(tenantConfig.BotUserId, tenantId);

                    tenantDto.BotUsername = user?.Nama;
                }
            }

            return tenantDto;
        }

        public async Task<TenantConfigModel?> FindTenantConfigByTenantIdAsync(string tenantId)
        {
            TenantConfigModel? tenantConfig = await dBContext.TenantConfig
            .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active) && a.TenantId.Equals(tenantId))
            .FirstOrDefaultAsync();

            return tenantConfig;
        }
        
        public async Task<string> GenerateKodeTenant()
        {
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Tenant.Where(a => a.TenantId.Equals(tenantId) && a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeTenant = $"{PrefixKodeConstant.Tenant}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodeTenant);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeTenant = $"{PrefixKodeConstant.Tenant}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeTenant);
            }
            return kodeTenant;
        }

        public async Task<TenantModel?> FindByKodeAsync(string kode)
        {
            TenantModel? tenant = await dBContext.Tenant
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return tenant;
        }

        public async Task GenerateDefaultData(TenantModel tenant, string masterTenantId)
        {
            using var transaction = dBContext.Database.BeginTransaction();
            try
            {
                //Add Metode Tunai Kasir
                var kodeMetodePembayaran = await metodePembayaranRepository.GenerateKodeMetodePembayaran();
                var newMetodePembayaran = new MetodePembayaranModel()
                {
                    Kode = kodeMetodePembayaran,
                    Nama = "Tunai Kasir",
                    DayJatuhTempo = 0,
                    RecordStatus = RecordStatusConstant.Active,
                    Deskripsi = "Pembayaran Untuk Mode Penjualan Kasir",
                };

                actionModelHelper.AssignCreateModel(newMetodePembayaran, "MetodePembayaran", userId, tenant.TenantId);
                dBContext.Add(newMetodePembayaran);

                var newMetodePembayaranTransfer = new MetodePembayaranModel()
                {
                    Kode = kodeMetodePembayaran.Substring(0, kodeMetodePembayaran.Length - 2) + "2",
                    Nama = "Transfer Bank",
                    DayJatuhTempo = 0,
                    RecordStatus = RecordStatusConstant.Active,
                    Deskripsi = "Pembayaran Untuk Transfer Bank",
                };

                actionModelHelper.AssignCreateModel(newMetodePembayaranTransfer, "MetodePembayaran", userId, tenant.TenantId);
                dBContext.Add(newMetodePembayaranTransfer);

                var kodeKurirToko = await kurirRepository.GenerateKodeKurir();
                var newKurir = new KurirModel()
                {
                    Kode = kodeKurirToko,
                    Nama = "Kurir Toko",
                    NoHP = "000",
                    IsMain = true,
                    Telepon = "000",
                    KontakPerson = "Admin",
                    RecordStatus = RecordStatusConstant.Active,
                    Deskripsi = "Kurir Utama Penjualan Kasir",
                };

                actionModelHelper.AssignCreateModel(newKurir, "Kurir", userId, tenant.TenantId);
                dBContext.Add(newKurir);

                var kodeGroupPelanggan = await groupPelangganRepository.GenerateKodeGroupPelanggan();
                var groupPelanggan = new GroupPelangganModel()
                {
                    Kode = kodeGroupPelanggan,
                    Nama = "Retail",
                    IsKonsinyasi = false,
                    RecordStatus = RecordStatusConstant.Active,
                    Deskripsi = "Group Pelanggan Retail",
                };

                actionModelHelper.AssignCreateModel(groupPelanggan, "GroupPelanggan", userId, tenant.TenantId);
                dBContext.Add(groupPelanggan);

                var kodeTipePelanggan = await tipePelangganRepository.GenerateKodeTipePelanggan();
                var tipePelanggan = new TipePelangganModel()
                {
                    Kode = kodeTipePelanggan,
                    Nama = "Perorangan",
                    RecordStatus = RecordStatusConstant.Active,
                    Deskripsi = "Tipe Pelanggan Perorangan",
                };

                actionModelHelper.AssignCreateModel(tipePelanggan, "TipePelanggan", userId, tenant.TenantId);
                dBContext.Add(tipePelanggan);

                var kodeJabatan = await jabatanRepository.GenerateKodeJabatan();
                var newJabatan = new JabatanModel()
                {
                    Kode = kodeJabatan,
                    Nama = "Admin",
                    TanggungJawab = "Admin",
                    Deskripsi = "Admin",
                    RecordStatus = RecordStatusConstant.Active
                };
                actionModelHelper.AssignCreateModel(newJabatan, "Jabatan", userId, tenant.TenantId);
                dBContext.Add(newJabatan);

                var defaultAlamat = await dBContext.Wilayah.FirstOrDefaultAsync(a => a.WilayahId.Equals("121dd5508f59423dbcc3b54a7935cca9"));

                var kodeGudang = await gudangRepository.GenerateKodeGudang();
                var newGudang = new GudangModel()
                {
                    WilayahId = defaultAlamat?.WilayahId,
                    Kode = kodeGudang,
                    Nama = "Gudang Utama",
                    Alamat = "Alamat Gudang Utama",
                    Telepon = "",
                    IsMain = true,
                    IsKonsinyasi = false,
                    PersonInCharge = "Admin Gudang",
                    Email = "",
                    Deskripsi = "Gudang utama",
                    RecordStatus = RecordStatusConstant.Active
                };
                actionModelHelper.AssignCreateModel(newGudang, "Gudang", userId, tenant.TenantId);
                dBContext.Add(newGudang);

                var newGudangKonsi = new GudangModel()
                {
                    WilayahId = defaultAlamat?.WilayahId,
                    Kode = kodeGudang.Substring(0, kodeGudang.Length - 2) + "2",
                    Nama = "Gudang Konsinyasi",
                    Alamat = tenant.Alamat ?? "",
                    Telepon = "",
                    IsMain = false,
                    IsKonsinyasi = true,
                    PersonInCharge = "Admin Gudang",
                    Email = "",
                    Deskripsi = "Gudang Konsinyasi",
                    RecordStatus = RecordStatusConstant.Active
                };
                actionModelHelper.AssignCreateModel(newGudangKonsi, "Gudang", userId, tenant.TenantId);
                dBContext.Add(newGudangKonsi);

                var userAdmin = await dBContext.User.FirstOrDefaultAsync(a => a.TenantId.Equals(tenant.TenantId) && a.Nama.StartsWith("Admin"));

                var kodeKaryawan = await karyawanRepository.GenerateKodeKaryawan();
                var newKaryawan = new KaryawanModel()
                {
                    Kode = kodeKaryawan,
                    Nama = "Admin",
                    Email = "admintoko@azzampos.com",
                    Alamat = "Alamat Admin",
                    TanggalLahir = DateTime.UtcNow.AddYears(-20),
                    TanggalJoin = DateTime.UtcNow,
                    JabatanId = newJabatan.JabatanId,
                    Deskripsi = "Admin Toko",
                    RecordStatus = RecordStatusConstant.Active,
                    WilayahId = defaultAlamat?.WilayahId,
                    NamaBank = "",
                    NoRekening = "",
                    NamaPemilik = "",
                    NoHp = "",
                    NoWa = "",
                    UserId = userAdmin?.UserId,
                    GudangId = newGudang.GudangId,
                    TelegramChatId = tenant.TelegramChatId,
                    Telegram = "",
                };

                actionModelHelper.AssignCreateModel(newKaryawan, "Karyawan", userId, tenant.TenantId);
                dBContext.Add(newKaryawan);

                var kodePelanggan = await pelangganRepository.GenerateKodePelanggan();
                var newPelanggan = new PelangganModel()
                {
                    GroupPelangganId = groupPelanggan.GroupPelangganId,
                    TipePelangganId = tipePelanggan.TipePelangganId,
                    Kode = kodePelanggan,
                    Nama = "Retail-Umum",
                    Alamat = tenant.Alamat ?? "",
                    WilayahId = defaultAlamat?.WilayahId,
                    Telepon = "No Telp",
                    NoHp = "No HP",
                    Telegram = "Telegram",
                    Email = "",
                    KaryawanId = newKaryawan.KaryawanId,
                    TanggalJoin = DateTime.UtcNow,
                    SumberAgen = "",
                    NamaTokoOffline = "",
                    NamaTokoOnline = "",
                    LinkTokoOnline = "",
                    Deskripsi = "Pelanggan Umum",
                    RecordStatus = RecordStatusConstant.Active
                };

                actionModelHelper.AssignCreateModel(newPelanggan, "Pelanggan", userId, tenant.TenantId);
                dBContext.Add(newPelanggan);

                // Generate Jenis Produk
                var listJenisProduk = await dBContext.Jenis.Where(a => a.TenantId == masterTenantId
                && a.RecordStatus.Equals(RecordStatusConstant.Active)).ToListAsync();

                var kodeJenisProduk = await jenisRepository.GenerateKodeJenis();

                var newJenisProduks = new List<JenisModel>();
                
                int indexKode = 1;
                foreach (var jenis in listJenisProduk)
                {
                    kodeJenisProduk = kodeJenisProduk.Substring(0, kodeJenisProduk.Length - 2) + indexKode.ToString();
                    indexKode += 1;

                    var newJenis = new JenisModel
                    {
                        TenantId = tenantId,
                        Nama = jenis.Nama,
                        Kode = kodeJenisProduk,
                        IsShowExpiredDate = jenis.IsShowExpiredDate,
                        IsShowVarian = jenis.IsShowVarian,
                        IsShowUkuran = jenis.IsShowUkuran,
                        IsShowWarna = jenis.IsShowWarna,
                        IsShowBerat = jenis.IsShowBerat,
                        IsShowTinggi = jenis.IsShowTinggi,
                        IsShowPanjang = jenis.IsShowPanjang,
                        IsShowLebar = jenis.IsShowLebar,
                        IsShowPomTR = jenis.IsShowPomTR,
                        IsShowKenaPajak = jenis.IsShowKenaPajak,
                        IsShowJualOnline = jenis.IsShowJualOnline,
                        Deskripsi = jenis.Deskripsi,
                        RecordStatus = RecordStatusConstant.Active
                    };
                    actionModelHelper.AssignCreateModel(newJenis, "Jenis", userId, tenant.TenantId);
                    newJenisProduks.Add(newJenis);
                }

                dBContext.AddRange(newJenisProduks);

                // Generate Satuan
                var listSatuan = await dBContext.Satuan.Where(a => a.TenantId == masterTenantId && a.RecordStatus.Equals(RecordStatusConstant.Active)).ToListAsync();
                var newSatuans = new List<SatuanModel>();

                var kodeSatuan = await satuanRepository.GenerateKodeSatuan();
                indexKode = 1;

                foreach (var satuan in listSatuan)
                {
                    kodeSatuan = kodeSatuan.Substring(0, kodeSatuan.Length - 2) + indexKode.ToString();
                    indexKode += 1;

                    var newSatuanModel = new SatuanModel
                    {
                        TenantId = tenantId,
                        Nama = satuan.Nama,
                        Kode = kodeSatuan,
                        Deskripsi = satuan.Deskripsi,
                        RecordStatus = RecordStatusConstant.Active
                    };

                    actionModelHelper.AssignCreateModel(newSatuanModel, "Satuan", userId, tenant.TenantId);
                    newSatuans.Add(newSatuanModel);
                }

                dBContext.AddRange(newSatuans);

                // Generate Merek
                var listMerek = await dBContext.Merek.Where(a => a.TenantId == masterTenantId && a.RecordStatus.Equals(RecordStatusConstant.Active)).ToListAsync();
                var newMereks = new List<MerekModel>();
                var kodeMerek = await merekRepository.GenerateKodeMerek();
                indexKode = 1;
                foreach (var merek in listMerek)
                {
                    kodeMerek = kodeMerek.Substring(0, kodeMerek.Length - 2) + indexKode.ToString();
                    indexKode += 1;

                    var newMerekModel = new MerekModel
                    {
                        TenantId = tenantId,
                        Nama = merek.Nama,
                        Kode = kodeMerek,
                        Deskripsi = merek.Deskripsi,
                        RecordStatus = RecordStatusConstant.Active
                    };
                    actionModelHelper.AssignCreateModel(newMerekModel, "Merek", userId, tenant.TenantId);
                    newMereks.Add(newMerekModel);
                }

                dBContext.AddRange(newMereks);

                // Generate Supplier
                var listSupplier = await dBContext.Supplier.Where(a => a.TenantId == masterTenantId && a.RecordStatus.Equals(RecordStatusConstant.Active)).ToListAsync();
                var newSuppliers = new List<SupplierModel>();

                var kodeSupplier = await supplierRepository.GenerateKodeSupplier();
                indexKode = 1;

                foreach (var supplier in listSupplier)
                {
                    kodeSupplier = kodeSupplier.Substring(0, kodeSupplier.Length - 2) + indexKode.ToString();
                    indexKode += 1;

                    var newSupplierModel = new SupplierModel
                    {
                        TenantId = tenantId,
                        Nama = supplier.Nama,
                        Kode = kodeSupplier,
                        Alamat = tenant.Alamat ?? "",
                        Telepon = supplier.Telepon,
                        Email = supplier.Email,
                        NamaBank = supplier.NamaBank,
                        NoRekening = supplier.NoRekening,
                        NamaPemilik = supplier.NamaPemilik,
                        NoHp = supplier.NoHp,
                        WilayahId = defaultAlamat?.WilayahId,
                        KontakPerson = supplier.KontakPerson,
                        Deskripsi = supplier.Deskripsi,
                        RecordStatus = RecordStatusConstant.Active
                    };
                    actionModelHelper.AssignCreateModel(newSupplierModel, "Supplier", userId, tenant.TenantId);
                    newSuppliers.Add(newSupplierModel);
                }

                dBContext.AddRange(newSuppliers);

                await dBContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (KnownException ex)
            {
                await transaction.RollbackAsync();

                throw new KnownException(ex.Message);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                activityLogService.SaveErrorLog(ex, "Create Default Tenant Data", userId, tenant.TenantId);
                throw new Exception(ex.Message);
            }

        }
    }
}