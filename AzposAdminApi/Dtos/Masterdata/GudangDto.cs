using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class GudangDto : BaseDto
    {
        public string? GudangId { get; set; }

        public string? TenantId { get; set; }

        public string WilayahId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        public string Alamat { get; set; } = string.Empty;

        [Required]
        public string Telepon { get; set; } = string.Empty;

        public string? Email { get; set; }

        public bool IsMain { get; set; }

        public bool IsKonsinyasi { get; set; }

        public string PersonInCharge { get; set; } = string.Empty;

        public virtual WilayahDto? Wilayah { get; set; }

        public string? AlamatLengkap
        {
            get
            {
                string alamatLengkap = Alamat;

                if (Wilayah != null)
                {
                    alamatLengkap = $"{Alamat}, {Wilayah.Display}";
                }

                return alamatLengkap;
            }
        }
    }

    public class GudangAddValidator : AbstractValidator<GudangDto>
    {
        public GudangAddValidator()
        {
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.Telepon).NotEmpty().WithMessage("Telepon Tidak Boleh Kosong");
            RuleFor(x => x.WilayahId).NotEmpty().WithMessage("Nama Wilayah Tidak Boleh Kosong");
        }
    }

    public class GudangEditValidator : AbstractValidator<GudangDto>
    {
        public GudangEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.GudangId).NotEmpty().WithMessage("Gudang Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.Telepon).NotEmpty().WithMessage("Telepon Tidak Boleh Kosong");
            RuleFor(x => x.WilayahId).NotEmpty().WithMessage("Nama Wilayah Tidak Boleh Kosong");
        }
    }
}