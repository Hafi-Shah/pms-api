namespace pms_api.Models.Requests
{
    public class RegisterCompanyReq
    {
        public string CompanyName { get; set; }
        public int CompanyTypeId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNum { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CountryId { get; set; }
        public string? ProfilePic { get; set; }
    }
}
