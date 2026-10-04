using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class WilayahDto : BaseDto
    {
        public string WilayahId { get; set; } = string.Empty;

        public string Kode { get; set; } = string.Empty;
        public string KodeProvinsi { get; set; } = string.Empty;

        public string KodeKabupatenKota { get; set; } = string.Empty;

        public string KodeKecamatan { get; set; } = string.Empty;

        public string KodeKelurahan { get; set; } = string.Empty;

        public string Provinsi { get; set; } = string.Empty;

        public string KabupatenKota { get; set; } = string.Empty;

        public string Kecamatan { get; set; } = string.Empty;

        public string Kelurahan { get; set; } = string.Empty;

        public string KodePos { get; set; } = string.Empty;

        public string Display
        {
            get
            {
                return $"Kel./Desa {Kelurahan}, Kec. {Kecamatan}, {KabupatenKota}, {Provinsi} - Kode Pos: {KodePos}";
            }
        }

        public string DisplayKabupaten
        {
            get
            {
                return $"{KabupatenKota}, {Provinsi}";
            }
        }

        public string DisplayKecamatan
        {
            get
            {
                return $"Kec. {Kecamatan}, {KabupatenKota}, {Provinsi}";
            }
        }

        public string DisplayKelurahan
        {
            get
            {
                return $"Kel./Desa {Kelurahan}, Kec. {Kecamatan}, {KabupatenKota}, {Provinsi}";
            }
        }
    }

    public class WilayahAddValidator : AbstractValidator<WilayahDto>
    {
        public WilayahAddValidator()
        {
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Provinsi).NotEmpty().WithMessage("Provinsi Tidak Boleh Kosong");
            RuleFor(x => x.KabupatenKota).NotEmpty().WithMessage("Kabupaten Kota Tidak Boleh Kosong");
            RuleFor(x => x.Kecamatan).NotEmpty().WithMessage("Kecamatan Tidak Boleh Kosong");
            RuleFor(x => x.Kelurahan).NotEmpty().WithMessage("Kelurahan Tidak Boleh Kosong");
            RuleFor(x => x.KodePos).NotEmpty().WithMessage("Kode Pos Tidak Boleh Kosong");
        }
    }

    public class WilayahEditValidator : AbstractValidator<WilayahDto>
    {
        public WilayahEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.WilayahId).NotEmpty().WithMessage("Wilayah Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Provinsi).NotEmpty().WithMessage("Provinsi Tidak Boleh Kosong");
            RuleFor(x => x.KabupatenKota).NotEmpty().WithMessage("Kabupaten Kota Tidak Boleh Kosong");
            RuleFor(x => x.Kecamatan).NotEmpty().WithMessage("Kecamatan Tidak Boleh Kosong");
            RuleFor(x => x.Kelurahan).NotEmpty().WithMessage("Kelurahan Tidak Boleh Kosong");
            RuleFor(x => x.KodePos).NotEmpty().WithMessage("Kode Pos Tidak Boleh Kosong");
        }
    }
}