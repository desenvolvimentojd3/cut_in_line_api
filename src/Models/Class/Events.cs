
namespace CutInLine.Models.Class
{
    public class Events
    {
        public int EventId { get; set; } = int.MinValue;
        public string SDescription { get; set; } = string.Empty;
        public string SDetail { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime DDateCreated { get; set; } = DateTime.Now;
        public Events()
        {
        }
    }
}
