namespace pms_api.Models.Requests
{
    public class RegUserReq
    {
        public string FirstName { get; set; }//
        public string LastName { get; set; }//
        public string Email { get; set; }//
        public string Password { get; set; }//
        public int YearlyExp { get; set; }//
        public string About { get; set; }//
        public int StatusId { get; set; }//
        public List<string> Skills { get; set; }//
        public DateTime Dob { get; set; }//
        public int GenderId { get; set; }//
        public int JobTitleId { get; set; }//
        public string Contact { get; set; }//
        public string ProfilePic { get; set; }//
        public string CoverImg { get; set; } //
    }
}
