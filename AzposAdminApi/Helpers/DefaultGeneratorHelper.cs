using AzposAdminApi.Constants;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Identity;

namespace AzposAdminApi.Helpers
{
    public class DefaultGeneratorHelper
    {
        private readonly ActionModelHelper actionModelHelper;
        private readonly PasswordHasherHelper passwordHasherHelper;
        private readonly AzPosDBContext dbContext;
        private readonly string userId, tenantId;

        public DefaultGeneratorHelper(string iuserId, string itenantId, AzPosDBContext idbContext)
        {
            passwordHasherHelper = new PasswordHasherHelper();
            actionModelHelper = new ActionModelHelper();

            userId = iuserId;
            tenantId = itenantId;
            dbContext = idbContext;
        }

        public async Task GenerateFirstAdmin(string nama)
        {
            var roleAll = await GenerateRoleAllAkses();
            var roleAdmin = await GenerateRoleAdmin();

            nama = nama.Replace(" ", "");
            nama = nama.Trim();
            nama = nama.ToLower();

            string inovasyAdmin = "Inovasy" + nama;
            string admin = "Admin" + nama;

            await GenerateUser(roleAll.RoleId, roleAll.Name, "Inovasy Admin " + nama, inovasyAdmin, "5017443038"); //IT Admin
            await GenerateUser(roleAdmin.RoleId, roleAdmin.Name, "Admin " + nama, admin, "5017443038"); //Admin 
        }

        public async Task<RoleModel> GenerateRoleAllAkses()
        {
            RoleModel roleAdmin = new()
            {
                TenantId = tenantId,
                Name = "Semua Akses",
                RecordStatus = RecordStatusConstant.Active,
            };

            actionModelHelper.AssignCreateModel(roleAdmin, "Role", userId, tenantId);

            dbContext.Add(roleAdmin);
            await dbContext.SaveChangesAsync();

            //add role modul
            var moduls = ModulMappingHelper.GetAllModul();
            var modulMaintenances = ModulMappingHelper.GetMaintenanceModul();
            moduls.AddRange(modulMaintenances);

            var modulToAdd = new List<RoleModulModel>();

            foreach (var modul in moduls)
            {
                if (modul.Name.Contains("Hide"))
                    continue;

                var roleModul = new RoleModulModel
                {
                    RoleId = roleAdmin.RoleId,
                    RoleName = roleAdmin.Name,
                    GroupModul = modul.GroupModul,
                    Group = modul.Group,
                    Modul = modul.Modul,
                    Name = modul.Name,
                    RecordStatus = RecordStatusConstant.Active
                };

                actionModelHelper.AssignCreateModel(roleModul, "RoleModul", userId, tenantId);

                modulToAdd.Add(roleModul);
            }

            dbContext.AddRange(modulToAdd);
            await dbContext.SaveChangesAsync();

            return roleAdmin;
        }

        public async Task<RoleModel> GenerateRoleAdmin()
        {
            RoleModel roleAdmin = new()
            {
                TenantId = tenantId,
                Name = "Admin Akses",
                RecordStatus = RecordStatusConstant.Active,
            };

            actionModelHelper.AssignCreateModel(roleAdmin, "Role", userId, tenantId);

            dbContext.Add(roleAdmin);
            await dbContext.SaveChangesAsync();

            //add role modul
            var moduls = ModulMappingHelper.GetAllModul();
            var modulToAdd = new List<RoleModulModel>();

            foreach (var modul in moduls)
            {
                if (ModulConstant.RoleForAdmin.Contains(modul.Name))
                {
                    var roleModul = new RoleModulModel
                    {
                        RoleId = roleAdmin.RoleId,
                        RoleName = roleAdmin.Name,
                        GroupModul = modul.GroupModul,
                        Group = modul.Group,
                        Modul = modul.Modul,
                        Name = modul.Name,
                        RecordStatus = RecordStatusConstant.Active
                    };

                    actionModelHelper.AssignCreateModel(roleModul, "RoleModul", userId, tenantId);

                    modulToAdd.Add(roleModul);
                }
            }

            dbContext.AddRange(modulToAdd);
            await dbContext.SaveChangesAsync();

            return roleAdmin;
        }

        public async Task<UserModel> GenerateUser(string roleId, string roleName, string nama, string userName, string telegramChatId, bool isUseTelegramForLogin = false)
        {
            UserModel userAdmin = new()
            {
                AccessFailedCount = 0,
                Nama = nama,
                Username = userName,
                Email = userName + "@azzamkita.com",
                LastAccessDate = DateTime.UtcNow,
                Phone = DateTime.UtcNow.Ticks.ToString(),
                IsLocked = false,
                RecordStatus = RecordStatusConstant.Active,
                EmailConfirmed = true,
                PhoneConfirmed = true,
                Deskripsi = "Default Admin From Generator",
                IsUseTelegramForLogin = isUseTelegramForLogin,
                TelegramChatId = telegramChatId,
                TenantId = tenantId,
                // Hash Password
                HashPassword = passwordHasherHelper.Hash(userName)
            };

            actionModelHelper.AssignCreateModel(userAdmin, "User", userId, tenantId);

            dbContext.Add(userAdmin);
            await dbContext.SaveChangesAsync();

            // Add User Roles
            UserRoleModel userRoleAdmin = new()
            {
                RoleId = roleId,
                Rolename = roleName,
                UserId = userAdmin.UserId,
                Username = roleName,
                RecordStatus = RecordStatusConstant.Active
            };

            actionModelHelper.AssignCreateModel(userRoleAdmin, "UserRole", userId, tenantId);

            dbContext.Add(userRoleAdmin);
            await dbContext.SaveChangesAsync();
            return userAdmin;
        }
    }
}