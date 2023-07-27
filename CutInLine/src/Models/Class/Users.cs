namespace CutInLine.Models.Class
{
    public class Users
    {
        public int? UserId { get; set; }
        public int? SLogin { get; set; }
        public string? SName { get; set; }
        public string? SHash { get; set; }
        public string? SAuthToken { get; set; }
        public string? SCellPhone { get; set; }
        public string? SPassWord { get; set; }
        public string? SCountry { get; set; }
        public DateTime? DDateCreated { get; set; }

        public Users()
        {
        }
    }
}
