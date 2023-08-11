
using CutInLine.Models.Class;

namespace CutInLine.Models.Interface
{
    public interface IEvents
    {
        Task<dynamic> Save(Events user, string token);
        Task<dynamic> Delete(int id, string token);
        Task<dynamic> GetById(int id, string token);
        Task<dynamic> GetEvents(SearchHelper search, string token);
    }
}
