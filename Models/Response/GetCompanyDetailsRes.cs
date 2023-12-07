namespace pms_api.Models.Response
{
    public class GetCompanyDetailsRes
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set;}
        public string? CompanyType { get; set; }
        public string? CompanyLocation { get; set; }
        public string? CompanyDescription { get; set; }
        public string? CountryName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? ContactNum { get; set; }
        public string? ProfilePic { get; set; }
        public string? CompanyPassword { get; set; }
    }
}
