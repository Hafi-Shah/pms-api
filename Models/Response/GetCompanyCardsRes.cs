namespace pms_api.Models.Response
{
    public class GetCompanyCardsRes
    {
        public int CompnanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyType { get; set; }
        public string? Country { get; set; }
        public string? ProfilePic { get; set; }
    }
}
