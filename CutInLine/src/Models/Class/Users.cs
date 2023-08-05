
namespace CutInLine.Models.Class
{
    public class Users
    {
        public int UserId { get; set; } = int.MinValue;
        public string SLogin { get; set; } = string.Empty;
        public string SName { get; set; } = string.Empty;
        public string SHash { get; set; } = string.Empty;
        public string SAuthToken { get; set; } = string.Empty;
        public string SCellPhone { get; set; } = string.Empty;
        public string SPassWord { get; set; } = string.Empty;
        public string SCountry { get; set; } = string.Empty;
        public DateTime DDateCreated { get; set; } = DateTime.MinValue;

        public Users()
        {
        }
    }
}
