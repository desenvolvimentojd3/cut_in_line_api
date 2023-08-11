using CutInLine.Models.Class.Helpers;

namespace CutInLine.Models.Class
{
    public class SearchHelper
    {
        public List<Where> Where { get; set; } = new List<Where>();
        public Pagination Pagination { get; set; } = new Pagination();
        public Ordination Ordination { get; set; } = new Ordination();
    }
}
