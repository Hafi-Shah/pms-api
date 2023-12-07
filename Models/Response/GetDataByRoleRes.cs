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
            public string? Title { get; set; }
            public string? UserType { get; set; }
            //public string? UserSkills { get; set; }
            public string? Gender { get; set; }
            public string? MartialStatus { get; set; }
            public string? Dob { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? TotalExperience { get; set; }
            public string? About { get; set; }
            public string? CurrentCompany { get; set; }
            public string? ContactNum { get; set; }
            public string? ProfilePic { get; set; }
            public string? CoverPic { get; set; }
            public string? Role { get; set; }
        }

    public class UpdateDataByRoleUserRes
    {
       
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? UserType { get; set; }
        //public string? UserSkills { get; set; }
        public string? Gender { get; set; }
        public string? MartialStatus { get; set; }
        public string? Dob { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? TotalExperience { get; set; }
        public string? About { get; set; }
        public string? CurrentCompany { get; set; }
        public string? ContactNum { get; set; }
        public string? ProfilePic { get; set; }
        //public string? CoverPic { get; set; }
       
    }
}
