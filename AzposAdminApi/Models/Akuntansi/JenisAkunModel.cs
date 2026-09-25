using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Akuntansi
{
    public class JenisAkunModel : BaseModel
    {
        [Key]
        public string JenisAkunId { get; set; } = string.Empty;

        public string KodeAwal { get; set; } = string.Empty;
        
        public string Nama { get; set; } = string.Empty;

        /// <summary>
        /// Reference To JenisAkunConstants
        /// </summary>/
        public string KodeConstant { get; set; } = string.Empty;
    }
}