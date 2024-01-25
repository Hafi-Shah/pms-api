namespace pms_api.Models.Response
{
    public class GetUserCards
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public int TotalExp { get; set; }
        public string? UserType { get; set; }
        public string? ProfilePic { get; set; }
        public string? CoverImg { get; set;}
        
    }
}
