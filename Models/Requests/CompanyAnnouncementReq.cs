namespace pms_api.Models.Requests
{
    public class CompanyAnnouncementReq
    {
        public int Exp { get; set;}
        public bool IsAutoApply { get; set;}
        public string Jd { get; set;}
        public List<int> SkillId { get; set; }
    }
}
