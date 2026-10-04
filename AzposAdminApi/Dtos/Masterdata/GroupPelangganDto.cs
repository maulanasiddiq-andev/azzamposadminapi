using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class GroupPelangganDto : BaseDto
    {
        public string GroupPelangganId { get; set; } = string.Empty;

        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;
        
        public bool IsKonsinyasi { get; set; }
    }

    public class GroupPellanganAddValidator : AbstractValidator<GroupPelangganDto>
    {
        public GroupPellanganAddValidator()
        {
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
        }
    }

    public class GroupPellanganEditValidator : AbstractValidator<GroupPelangganDto>
    {
        public GroupPellanganEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.GroupPelangganId).NotEmpty().WithMessage("Group Pellangan Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
        }
    }
}