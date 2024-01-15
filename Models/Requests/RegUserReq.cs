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
        public List<int> Skills { get; set; }//
        public DateTime Dob { get; set; }//
        public int GenderId { get; set; }//
        public string Contact { get; set; }//
        public string ProfilePic { get; set; }//
        public string CoverImg { get; set; } //
        public int UserTypeId { get; set; }
        public string Role { get; set; }
        public string City { get; set; }
        public int maritalStatusId { get; set; }
    }
}
