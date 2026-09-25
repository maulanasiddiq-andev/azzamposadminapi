using FluentValidation;

namespace AzposAdminApi.Dtos.Akuntansi
{
    public class JenisAkunDto : BaseDto
    {
        public string? JenisAkunId { get; set; }

        public string KodeAwal { get; set; } = string.Empty;

        public string Nama { get; set; } = string.Empty;

        /// <summary>
        /// Reference To JenisAkunConstants
        /// </summary>/
        public string KodeConstant { get; set; } = string.Empty;
        
        public bool IsShowBank { get; set; }
    }

    public class JenisAkunAddValidator : AbstractValidator<JenisAkunDto>
    {
        public JenisAkunAddValidator()
        {
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.KodeAwal).NotEmpty().WithMessage("Kode Awal Tidak Boleh Kosong");
            RuleFor(x => x.KodeConstant).NotEmpty().WithMessage("Kode Constant Awal Tidak Boleh Kosong");
        }
    }

    public class JenisAkunEditValidator : AbstractValidator<JenisAkunDto>
    {
        public JenisAkunEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.JenisAkunId).NotEmpty().WithMessage("Jenis Akun Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.KodeAwal).NotEmpty().WithMessage("Kode Awal Tidak Boleh Kosong");
            RuleFor(x => x.KodeConstant).NotEmpty().WithMessage("Kode Constant Awal Tidak Boleh Kosong");
        }
    }
}