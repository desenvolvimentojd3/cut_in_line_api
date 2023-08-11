
namespace CutInLine.Models.Class
{
    public class Products
    {
        public int ProductId { get; set; } = int.MinValue;
        public string SDescription { get; set; } = string.Empty;
        public string SDetail { get; set; } = string.Empty;
        public string SGroupName { get; set; } = string.Empty;
        public decimal NValue { get; set; } = 0;
        public string Token { get; set; } = string.Empty;
        public DateTime DDateCreated { get; set; } = DateTime.Now;

        public Products()
        {
        }
    }
}
