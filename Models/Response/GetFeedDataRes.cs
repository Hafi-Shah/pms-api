namespace pms_api.Models.Response
{
    public class GetFeedData
    {
        public int companyId { get; set; }
        public string? companyName { get; set; }
        public string? companyType { get; set; }
        public string? country { get; set; }
        public string? jobDescription { get; set; }
        public int yearOfExp { get; set; }
        //public List<int>? skillsList { get; set; }
        public string? skills { get; set; }
    }

    public class GetFeedDataRes
    {
        public int companyId { get; set; }
        public string? companyName { get; set; }
        public string? companyType { get; set; }
        public string? country { get; set; }
        public string? jobDescription { get; set; }
        public int yearOfExp { get; set; }
        public List<string> skills { get; set; } = new List<string> { };
        public string? profilePic { get; set; }
    }
}
