    namespace pms_api.Models.Response
    {
        public class GetDataByRoleCompanyRes
        {
            public int UserId { get; set; }
            public string? CompanyName { get; set; }
            public string? CompanyType { get; set; }
            public string? CompanyLocation { get; set; }
            public string? CompanyDescription { get; set; }
            public string? CountryName { get; set; }
            public string? CompanyEmail { get; set; }
            public string? ContactNum { get; set; }
            public string? ProfilePic { get; set; }
            public string? Password { get; set; }
        //public string? Role { get; set; }

    }
    public class UpdateDataByRoleCompanyRes
    {
        public int UserId { get; set; }
        public string? CompanyName { get; set; }
        public int CompanyType { get; set; }
        public string? CompanyLocation { get; set; }
        public string? CompanyDescription { get; set; }
        public int CountryName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? ContactNum { get; set; }
        public string? ProfilePic { get; set; }
        public string? CompanyPassword { get; set; }
        

    }

    public class GetDataByRoleUserRes
        {
            public int UserId { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? UserType { get; set; }
            public List<string> UserSkills { get; set; } = new List<string> { };
            public string? Gender { get; set; }
            public string? MartialStatus { get; set; }
            public DateTime? Dob { get; set; }
            public string? City { get; set; }
            public string? Email { get; set; }
            public int? TotalExperience { get; set; }
            public string? About { get; set; }
            public string? ContactNum { get; set; }
            public string? ProfilePic { get; set; }
            public string? CoverPic { get; set; }
        }

    public class UpdateDataByRoleUserRes
    {

        public string FirstName { get; set; }//
        public string LastName { get; set; }//
        public string Email { get; set; }//
        public string Password { get; set; }//
        public int YearlyExp { get; set; }//
        public string About { get; set; }//
        public int StatusId { get; set; }//
        public List<int> UserSkills { get; set; } = new List<int> { };
        public DateTime Dob { get; set; }//
        public int GenderId { get; set; }//
        public int JobTitleId { get; set; }//
        public string Contact { get; set; }//
        public string ProfilePic { get; set; }//
        public string CoverImg { get; set; } //
        public int UserTypeId { get; set; }
        public string Role { get; set; }

        public string City { get; set; }

    }
}
