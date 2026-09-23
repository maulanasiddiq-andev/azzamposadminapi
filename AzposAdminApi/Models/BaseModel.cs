using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models
{
    public class BaseModel
    {
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint Version { get; set; }

        public string? Deskripsi { get; set; }

        public string RecordStatus { get; set; } = string.Empty;
        
        public DateTime CreatedTime { get; set; }
        
        public DateTime ModifiedTime { get; set; }
        
        public string CreatedBy { get; set; } = string.Empty;
        
        public string ModifiedBy { get; set; } = string.Empty;
    }
}