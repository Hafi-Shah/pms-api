namespace pms_api.Models.Response
{
    public class LoginRes
    {
        public string? Token { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }

    }
}
